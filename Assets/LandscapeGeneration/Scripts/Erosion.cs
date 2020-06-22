using UnityEngine;

public class Erosion : MonoBehaviour {

    public int seed;
    [Range (2, 8)] // задает спектр значений от 2 до 8
    public int erosionRadius = 3;
    [Range (0, 1)]
    public float inertia = .05f; // At zero, water will instantly change direction to flow downhill. At 1, water will never change direction. 
    public float sedimentCapacityFactor = 4; // коэффициент емкости осадка. Multiplier for how much sediment a droplet can carry
    public float minSedimentCapacity = .01f; // Used to prevent carry capacity getting too close to zero on flatter terrain
    [Range (0, 1)]
    public float erodeSpeed = .3f;
    [Range (0, 1)]
    public float depositSpeed = .3f; // скорость осадков
    [Range (0, 1)]
    public float evaporateSpeed = .01f; // скорость испарения
    public float gravity = 4;
    public int maxDropletLifetime = 30; // время существования одной капли

    public float initialWaterVolume = 1;
    public float initialSpeed = 1;

	// Indices and weights of erosion brush precomputed for every node
	// Индексы и веса кисти эрозии предварительно вычисляются для каждого узла
	int[][] erosionBrushIndices;
    float[][] erosionBrushWeights;
    System.Random prng;

    int currentSeed;
    int currentErosionRadius;
    int currentMapSize;

	// Initialization creates a System.Random object and precomputes indices and weights of erosion brush
	// Функция Initialize создает System.Random-объект (зерно) и предварительно вычисляет индексы и веса эрозионной кисти
	void Initialize (int mapSize, bool resetSeed) {
        if (resetSeed || prng == null || currentSeed != seed) {
            prng = new System.Random (seed);
            currentSeed = seed;
        }

        if (erosionBrushIndices == null || currentErosionRadius != erosionRadius || currentMapSize != mapSize) {
            InitializeBrushIndices (mapSize, erosionRadius);
            currentErosionRadius = erosionRadius;
            currentMapSize = mapSize;
        }
    }

    public void Erode (float[] map, int mapSize, int numIterations = 1, bool resetSeed = false) {
        Initialize (mapSize, resetSeed);

        for (int iteration = 0; iteration < numIterations; iteration++) {
			// Create water droplet at random point on map
			// Создание параметров капли воды, которая появится в случайной точке карты
			float posX = prng.Next (0, mapSize - 1); // случайная точка по X
            float posY = prng.Next (0, mapSize - 1); // случайная точка по Y
			float dirX = 0; // Направление по X
			float dirY = 0;  // Направление по Y
			float speed = initialSpeed; // Инициализация скорости капли
            float water = initialWaterVolume; // Инициализация количества воды в капле
			float sediment = 0; // количество переносимого каплей осадка

			
            for (int lifetime = 0; lifetime < maxDropletLifetime; lifetime++) {
                int nodeX = (int) posX; // случайная позиция X
                int nodeY = (int) posY; // случайная позиция Y
				int dropletIndex = nodeY * mapSize + nodeX; // индекс капли
                // Calculate droplet's offset inside the cell (0,0) = at NW node, (1,1) = at SE node
				// Вычисление положения капли в одной клетке
                float cellOffsetX = posX - nodeX;
                float cellOffsetY = posY - nodeY;

				// Calculate droplet's height and direction of flow with bilinear interpolation of surrounding heights
				// Вычисление высоты капель и направления потока с билинейной интерполяцией окружающих высот
				HeightAndGradient heightAndGradient = CalculateHeightAndGradient (map, mapSize, posX, posY);

				// Update the droplet's direction and position (move position 1 unit regardless of speed)
				// Обновление направления и положения капель (перемещение позиции на 1 независимо от скорости)
				dirX = (dirX * inertia - heightAndGradient.gradientX * (1 - inertia));
                dirY = (dirY * inertia - heightAndGradient.gradientY * (1 - inertia));
                // Normalize direction
                float len = Mathf.Sqrt (dirX * dirX + dirY * dirY);
                if (len != 0) {
                    dirX /= len;
                    dirY /= len;
                }
                posX += dirX;
                posY += dirY;

				// Stop simulating droplet if it's not moving or has flowed over edge of map
				// Остановить моделирование капли, если она не движется или перетекла через край карты
				if ((dirX == 0 && dirY == 0) || posX < 0 || posX >= mapSize - 1 || posY < 0 || posY >= mapSize - 1) {
                    break;
                }

				// Find the droplet's new height and calculate the deltaHeight
				// Нахождение новой высоты капель и вычисление Дельта-высоты
				float newHeight = CalculateHeightAndGradient (map, mapSize, posX, posY).height;
                float deltaHeight = newHeight - heightAndGradient.height;

				// Calculate the droplet's sediment capacity (higher when moving fast down a slope and contains lots of water)
				// Расчет емкости осадка капель (выше при быстром движении вниз по склону и содержит много воды)
				float sedimentCapacity = Mathf.Max (-deltaHeight * speed * water * sedimentCapacityFactor, minSedimentCapacity);

				// If carrying more sediment than capacity, or if flowing uphill:
				// Если несете больше наносов, чем емкость, или если течет в гору:
				if (sediment > sedimentCapacity || deltaHeight > 0) {
					// If moving uphill (deltaHeight > 0) try fill up to the current height, otherwise deposit a fraction of the excess sediment
					// При движении в гору (deltaHeight > 0) попробуйте заполнить до текущей высоты, в противном случае отложите часть избыточного осадка
					float amountToDeposit = (deltaHeight > 0) ? Mathf.Min (deltaHeight, sediment) : (sediment - sedimentCapacity) * depositSpeed;
                    sediment -= amountToDeposit;

					// Add the sediment to the four nodes of the current cell using bilinear interpolation
					// Deposition is not distributed over a radius (like erosion) so that it can fill small pits
					// Добавьте осадок в четыре узла текущей ячейки с помощью билинейной интерполяции
					// Осаждение не распределяется по радиусу (как эрозия), так что он может заполнить небольшие ямы
					map[dropletIndex] += amountToDeposit * (1 - cellOffsetX) * (1 - cellOffsetY);
                    map[dropletIndex + 1] += amountToDeposit * cellOffsetX * (1 - cellOffsetY);
                    map[dropletIndex + mapSize] += amountToDeposit * (1 - cellOffsetX) * cellOffsetY;
                    map[dropletIndex + mapSize + 1] += amountToDeposit * cellOffsetX * cellOffsetY;

                } else {
					// Erode a fraction of the droplet's current carry capacity.
					// Clamp the erosion to the change in height so that it doesn't dig a hole in the terrain behind the droplet
					// Выветривается часть капель тока, пропускная способность.
					// Зажмите размывание к изменению в высоте так, что она не выкопает отверстие в местности за каплей
					float amountToErode = Mathf.Min ((sedimentCapacity - sediment) * erodeSpeed, -deltaHeight);

					// Use erosion brush to erode from all nodes inside the droplet's erosion radius
					// Используйте щетку размывания для того чтобы выветриться от всех узлов внутри радиуса размывания капелек
					for (int brushPointIndex = 0; brushPointIndex < erosionBrushIndices[dropletIndex].Length; brushPointIndex++) {
                        int nodeIndex = erosionBrushIndices[dropletIndex][brushPointIndex];
                        float weighedErodeAmount = amountToErode * erosionBrushWeights[dropletIndex][brushPointIndex];
                        float deltaSediment = (map[nodeIndex] < weighedErodeAmount) ? map[nodeIndex] : weighedErodeAmount;
                        map[nodeIndex] -= deltaSediment;
                        sediment += deltaSediment;
                    }
                }

				// Update droplet's speed and water content
				// Обновление скорости капель и содержания воды
				speed = Mathf.Sqrt (speed * speed + deltaHeight * gravity);
                water *= (1 - evaporateSpeed);
            }
        }
    }

    HeightAndGradient CalculateHeightAndGradient (float[] nodes, int mapSize, float posX, float posY) {
        int coordX = (int) posX;
        int coordY = (int) posY;

        // Calculate droplet's offset inside the cell (0,0) = at NW node, (1,1) = at SE node
        float x = posX - coordX;
        float y = posY - coordY;

        // Calculate heights of the four nodes of the droplet's cell
        int nodeIndexNW = coordY * mapSize + coordX;
        float heightNW = nodes[nodeIndexNW];
        float heightNE = nodes[nodeIndexNW + 1];
        float heightSW = nodes[nodeIndexNW + mapSize];
        float heightSE = nodes[nodeIndexNW + mapSize + 1];

        // Calculate droplet's direction of flow with bilinear interpolation of height difference along the edges
        float gradientX = (heightNE - heightNW) * (1 - y) + (heightSE - heightSW) * y;
        float gradientY = (heightSW - heightNW) * (1 - x) + (heightSE - heightNE) * x;

        // Calculate height with bilinear interpolation of the heights of the nodes of the cell
        float height = heightNW * (1 - x) * (1 - y) + heightNE * x * (1 - y) + heightSW * (1 - x) * y + heightSE * x * y;

        return new HeightAndGradient () { height = height, gradientX = gradientX, gradientY = gradientY };
    }

    void InitializeBrushIndices (int mapSize, int radius) {
        erosionBrushIndices = new int[mapSize * mapSize][]; // индекс для каждой точки карты для кистей
        erosionBrushWeights = new float[mapSize * mapSize][]; //индекс для каждой точки карты для весов

		int[] xOffsets = new int[radius * radius * 4];
        int[] yOffsets = new int[radius * radius * 4];
        float[] weights = new float[radius * radius * 4];
        float weightSum = 0;
        int addIndex = 0;

        for (int i = 0; i < erosionBrushIndices.GetLength (0); i++) {
            int centreX = i % mapSize;
            int centreY = i / mapSize;

            if (centreY <= radius || centreY >= mapSize - radius || centreX <= radius + 1 || centreX >= mapSize - radius) {
                weightSum = 0;
                addIndex = 0;
                for (int y = -radius; y <= radius; y++) {
                    for (int x = -radius; x <= radius; x++) {
                        float sqrDst = x * x + y * y;
                        if (sqrDst < radius * radius) {
                            int coordX = centreX + x;
                            int coordY = centreY + y;

                            if (coordX >= 0 && coordX < mapSize && coordY >= 0 && coordY < mapSize) {
                                float weight = 1 - Mathf.Sqrt (sqrDst) / radius;
                                weightSum += weight;
                                weights[addIndex] = weight;
                                xOffsets[addIndex] = x;
                                yOffsets[addIndex] = y;
                                addIndex++;
                            }
                        }
                    }
                }
            }

            int numEntries = addIndex;
            erosionBrushIndices[i] = new int[numEntries];
            erosionBrushWeights[i] = new float[numEntries];

            for (int j = 0; j < numEntries; j++) {
                erosionBrushIndices[i][j] = (yOffsets[j] + centreY) * mapSize + xOffsets[j] + centreX;
                erosionBrushWeights[i][j] = weights[j] / weightSum;
            }
        }
    }

    struct HeightAndGradient {
        public float height;
        public float gradientX;
        public float gradientY;
    }
}