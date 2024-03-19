import itertools
from math import copysign

# eps используется для задании точности при сравнении действительных чисел
# точность никак нельзя изменить в приложении, изменить можно только в коде программы
# с помощью точности программа определяет некоторые свойства точки, например, совпадает ли точка с одной из вершин треугольника 
eps = 10**(-3)

padding = 50

#region Triangle class
class Triangle:
    def __init__(self, vertices, points):
        self.vertices = vertices # вершины треугольника
        self.points = points # множество всех точек
        self.inner_points = self.find_inner_points() # точки, находящиеся внутри данного треугольника (за исключением самих вершин треугольника)

    """
    Преобразование координат происходит по следующему принципу:
    1)Находится максимальное по модулю значение X и Y среди всех заданных точек
    2)Все поле (canvas) делится на некоторые участки
    Длина участка выбирается таким образом, чтобы поле раделилось на 4 части (как координатные четверти)
    и в эти 4 части ГАРАНТИРОВАННО смогли поместиться все точки.
    3)Из пункта 2 следует, что scale (длина участка) должна быть минимальной из следующих вершин
    scale_x = canvas_width / (2 * max_x)
    scale_y = canvas_height / (2 * max_y)
    scale = min(scale_x, scale_y)
    """
    
    # Преобразование координаты X
    def x_transpose(self, x, scale, canvas_width):
        return 20 + x * scale + canvas_width / 2
        
    # Преобразование координаты Y
    def y_transpose(self, y, scale, canvas_height):
        return 20 + canvas_height / 2 - y * scale

    # Метод написан для отладочной версии
    def __str__(self) -> str:
        inner_points_str = "\n".join([f"({point[0]}, {point[1]}, {point[2]})" for point in self.inner_points])
        return f"Triangle: ({self.vertices[0][0]}, {self.vertices[0][1]}, {self.vertices[0][2]}); ({self.vertices[1][0]}, {self.vertices[1][1]}, {self.vertices[1][2]}); ({self.vertices[2][0]}, {self.vertices[2][1]}, {self.vertices[2][2]})"\
        + f"\nInner Points:\n{inner_points_str}\n"

    # Функция находит и возвращает в множестве заданых точек только те точки, которые находятся внутри треугольника
    # Используется правило пошаговых векторных произведений
    # Если у конкретной точки среди векторных произведений есть те, которые равны 0, значит конкретная точка лежит на стороне(-ах) треугольника
    # Конкретно вершины исходного треугольника не считаются за внутренние точки
    def find_inner_points(self):
        inner_points = []
        for point in self.points:
            if point[0] not in [v[0] for v in self.vertices]:
                x, y = point[1], point[2]
                x1, y1 = self.vertices[0][1], self.vertices[0][2]
                x2, y2 = self.vertices[1][1], self.vertices[1][2]
                x3, y3 = self.vertices[2][1], self.vertices[2][2]

                # |(x1-x3)  (y1-y3)|
                # |(x2-x3)  (y2-y3)|
                def sign(x1, y1, x2, y2, x3, y3):
                    return (x1 - x3) * (y2 - y3) - (x2 - x3) * (y1 - y3)

                d1 = sign(x, y, x1, y1, x2, y2)
                d2 = sign(x, y, x2, y2, x3, y3)
                d3 = sign(x, y, x3, y3, x1, y1)

                has_neg = (d1 < 0) or (d2 < 0) or (d3 < 0)
                has_pos = (d1 > 0) or (d2 > 0) or (d3 > 0)
                if (not (has_neg and has_pos)) or (abs(d1)<=eps and (not (has_neg and has_pos)))\
                    or (abs(d2)<=eps and (not (has_neg and has_pos))) or (abs(d3)<=eps and (not (has_neg and has_pos)))\
                        or (( 
                            (abs(x-x1)<=eps and abs(y-y1)<=eps) or
                            (abs(x-x2)<=eps and abs(y-y2)<=eps) or
                            (abs(x-x3)<=eps and abs(y-y3)<=eps)
                                                                )):
                    inner_points.append(point)
        return inner_points
    
    def find_median_intersection(self):
        x1, y1 = self.vertices[0][1], self.vertices[0][2]
        x2, y2 = self.vertices[1][1], self.vertices[1][2]
        x3, y3 = self.vertices[2][1], self.vertices[2][2]
        # Найти координаты центра противоположной стороны
        center_x = (x3 - x2) / 2 + x2
        center_y = (y3 - y2) / 2 + y2
        # Найти координаты точки, которая делит медиану в отношении 2:1
        median_num = 101 #debug_value
        median_x = (center_x - x1) / 3 * 2 + x1
        median_y = (center_y - y1) / 3 * 2 + y1
        return (median_num, median_x, median_y)

    def task_property(self):
        # Точка пересечения медиан
        median_intersection = self.find_median_intersection()
        # Середины каждой из сторон
        side1_num = 102 #debug_value
        side1_center_x = (self.vertices[0][1] + self.vertices[1][1]) / 2
        side1_center_y = (self.vertices[0][2] + self.vertices[1][2]) / 2
        side2_num = 103 #debug_value
        side2_center_x = (self.vertices[1][1] + self.vertices[2][1]) / 2
        side2_center_y = (self.vertices[1][2] + self.vertices[2][2]) / 2
        side3_num = 104 #debug_value
        side3_center_x = (self.vertices[2][1] + self.vertices[0][1]) / 2
        side3_center_y = (self.vertices[2][2] + self.vertices[0][2]) / 2
        # Формируем 6 внутренних треугольников
        triangle1 = Triangle([median_intersection, (side1_num, side1_center_x, side1_center_y), self.vertices[0]], self.inner_points)
        triangle2 = Triangle([median_intersection, (side2_num, side2_center_x, side2_center_y), self.vertices[1]], self.inner_points)
        triangle3 = Triangle([median_intersection, (side3_num, side3_center_x, side3_center_y), self.vertices[2]], self.inner_points)
        triangle4 = Triangle([median_intersection, (side1_num, side1_center_x, side1_center_y), self.vertices[1]], self.inner_points)
        triangle5 = Triangle([median_intersection, (side2_num, side2_center_x, side2_center_y), self.vertices[2]], self.inner_points)
        triangle6 = Triangle([median_intersection, (side3_num, side3_center_x, side3_center_y), self.vertices[0]], self.inner_points)
        # print(triangle1)
        # print(triangle2)
        # print(triangle3)
        # print(triangle4)
        # print(triangle5)
        # print(triangle6)
        # Подсчет внутренних точек всех 6 треугольников
        inner_dots_counts = [
            len(triangle1.inner_points),
            len(triangle2.inner_points),
            len(triangle3.inner_points),
            len(triangle4.inner_points),
            len(triangle5.inner_points),
            len(triangle6.inner_points)
        ]

        # Считаем максимум и минимум внутренних точек
        max_inner_dots_count = max(inner_dots_counts)
        min_inner_dots_count = min(inner_dots_counts)
        # print(max_inner_dots_count, min_inner_dots_count)

        # В данном блоке кода находятся максимальный и минимальный по количеству внутренних точек под-треугольники
        # Они используются в дальнейшем для отрисовки разными цветами на поле (canvas)
        tr = [triangle1, triangle2, triangle3, triangle4, triangle5, triangle6]
        max_triangle = triangle1
        min_triangle = triangle1
        for i in range(6):
            if (len(tr[i].inner_points) > len(max_triangle.inner_points)):
                max_triangle = tr[i]
            if (len(tr[i].inner_points) <= len(min_triangle.inner_points)):
                min_triangle = tr[i]
        return max_inner_dots_count - min_inner_dots_count, max_triangle, min_triangle
    
    # Метод рисует треугольник по следущим критериям:
    """
    Вершины и стороны - красный цвет
    Медианы - синий цвет
    Внутренние точки - зеленый цвет
    Максимальный под-треугольник - фиолетовый цвет
    Минимальный под-треугольник - оранжевый цвет

    Рядом с каждой точкой пишется её номер и координаты
    """
    
    def draw_triangle(self, canvas):
        canvas.delete("all")
        # Отступ
        canvas_width = canvas.winfo_width() - 50  # Padding
        canvas_height = canvas.winfo_height() - 50  # Padding

        #region Получение информации о поле для правильного масштабирования изображения
        max_x = max(abs(point[1]) for point in self.points)
        max_y = max(abs(point[2]) for point in self.points)

        scale_x = canvas_width / (2 * max_x)
        scale_y = canvas_height / (2 * max_y)

        scale = min(scale_x, scale_y)
        #endregion

        # Отрисовка треугольника исходного
        canvas.create_polygon(self.x_transpose(self.vertices[0][1], scale, canvas_width), self.y_transpose(self.vertices[0][2], scale, canvas_height), 
                              self.x_transpose(self.vertices[1][1], scale, canvas_width), self.y_transpose(self.vertices[1][2], scale, canvas_height), 
                              self.x_transpose(self.vertices[2][1], scale, canvas_width), self.y_transpose(self.vertices[2][2], scale, canvas_height), outline="red", fill="", width=2)

        # Отрисовка медиан исходного треугольника
        for i in range(3):
            opposite_vertex = (i + 2) % 3  # Получить индекс противоположной вершины
            median_x = (self.x_transpose(self.vertices[opposite_vertex][1], scale, canvas_width) - self.x_transpose(self.vertices[i][1], scale, canvas_width)) / 2 + self.x_transpose(self.vertices[i][1], scale, canvas_width)
            median_y = (self.y_transpose(self.vertices[opposite_vertex][2], scale, canvas_height) - self.y_transpose(self.vertices[i][2], scale, canvas_height)) / 2 + self.y_transpose(self.vertices[i][2], scale, canvas_height)
            canvas.create_line(self.x_transpose(self.vertices[(i+1)%3][1], scale, canvas_width), self.y_transpose(self.vertices[(i+1)%3][2], scale, canvas_height), 
                               median_x, median_y, fill="blue")

        # Возврат максимального значения разницы и отрисовка максимального и минимального подтреугольников
        max_triangle, max_property, a, b = find_maximum_task_property([self])
        deb, max_triangle, min_triangle = self.task_property()
        global padding
        #padding = 0
        canvas.create_polygon(max_triangle.x_transpose(max_triangle.vertices[0][1], scale, canvas_width), max_triangle.y_transpose(max_triangle.vertices[0][2], scale, canvas_height), 
                              max_triangle.x_transpose(max_triangle.vertices[1][1], scale, canvas_width), max_triangle.y_transpose(max_triangle.vertices[1][2], scale, canvas_height), 
                              max_triangle.x_transpose(max_triangle.vertices[2][1], scale, canvas_width), max_triangle.y_transpose(max_triangle.vertices[2][2], scale, canvas_height), outline="darkorchid1", fill="", width=2)
        canvas.create_polygon(min_triangle.x_transpose(min_triangle.vertices[0][1], scale, canvas_width), min_triangle.y_transpose(min_triangle.vertices[0][2], scale, canvas_height), 
                              min_triangle.x_transpose(min_triangle.vertices[1][1], scale, canvas_width), min_triangle.y_transpose(min_triangle.vertices[1][2], scale, canvas_height), 
                              min_triangle.x_transpose(min_triangle.vertices[2][1], scale, canvas_width), min_triangle.y_transpose(min_triangle.vertices[2][2], scale, canvas_height), outline="darkorange", fill="", width=2)
        padding = 50
        # Отрисовка вершин
        for point in self.vertices:
            x = self.x_transpose(point[1], scale, canvas_width)
            y = self.y_transpose(point[2], scale, canvas_height)
            canvas.create_oval(x - 5, y - 5, x + 5, y + 5, fill="red")
            canvas.create_text(x - (15*copysign(1, point[1])), y + 10*copysign(1, point[2]), text=str(f"{point[0]}: \n({point[1]}; {point[2]})"))  # Текст рядом с самой точкой

        # Отрисовка внутренних точек
        for point in self.inner_points:
            x = self.x_transpose(point[1], scale, canvas_width)
            y = self.y_transpose(point[2], scale, canvas_height)
            canvas.create_oval(x - 2, y - 2, x + 2, y + 2, fill="green")
            canvas.create_text(x - 15*copysign(1, point[1]), y + 10*copysign(1, point[2]), text=str(f"{point[0]}: \n({point[1]}; {point[2]})"))

        return max_property
#endregion

# Функция находит среди всех объектов класса Triangle тот, в котором метод task_property() возвращает максимальное значение
def find_maximum_task_property(triangles):
    max_property = float('-inf')
    max_triangle = None
    for triangle in triangles:
        property_value, a, b = triangle.task_property()
        if property_value > max_property:
            max_property = property_value
            max_triangle = triangle
            pa = a
            pb = b

    return max_triangle, max_property, pa, pb

#region points to Triangle funcs

# Расстояние между двумя точками
# sqrt((X1 - X2)^2 + (Y1 - Y2)^2)
def distance(p1, p2):
    return ((p1[1] - p2[1])**2 + (p1[2] - p2[2])**2)**0.5

def is_valid_triangle(triangle_points):
    # Вычислить все расстояния между всеми парами точек
    distances = [distance(triangle_points[i], triangle_points[j]) for i in range(3) for j in range(i+1, 3)]
    # Возвращаем True, если выполняется неравенство треугольника для всех сторон
    # Каждая сторона треугольника должна быть меньше суммы двух других его сторон
    return all(distances[i] + distances[j] - distances[3-i-j] > eps for i in range(3) for j in range(i+1, 3))

# Функция создает объект класса Triangle на основе информации о всех точках и о каких-то трех из них 
def create_triangle_from_points(triangle_points, points):
    vertices = []
    for point in triangle_points:
        vertices.append([point[0], point[1], point[2]])
    return Triangle(vertices, points)

# Функция создает массив из всевозможных объектов класса Triangle
def create_triangles_from_points(points):
    triangles = []
    point_indices = [point[0] for point in points]
    point_combinations = list(itertools.combinations(point_indices, 3))
    for combination in point_combinations:
        triangle_points = [point for point in points if point[0] in combination]
        if is_valid_triangle(triangle_points):  # Можно ли построить на данных 3-х точках треугольник? (исключаем вырожденный случай)
            triangle = create_triangle_from_points(triangle_points, points)
            triangles.append(triangle)
    return triangles

#endregion