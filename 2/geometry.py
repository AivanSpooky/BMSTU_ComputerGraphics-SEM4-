import tkinter as tk
import math

class Ellipse():
    def __init__(self, canvas: tk.Canvas, x, y, width, height, tag: str):
        self.x = x
        self.y = y
        self.width = width
        self.height = height
        self.angle = 0  # Угол поворота эллипса
        self.canvas = canvas
        self.tag = f"ellipse-{tag}"
        self.draw_self()

    def draw_self(self):
        # Очищаем холст (старую версию эелмента)
        self.canvas.delete(self.tag)
        # Количество точек для отображения эллипса
        # num_points = 50
        num_points = 1000
        # Вычисляем параметры для построения эллипса
        cos_angle = math.cos(math.radians(self.angle))
        sin_angle = math.sin(math.radians(self.angle))
        half_width = self.width / 2
        half_height = self.height / 2
        # Рассчитываем координаты точек эллипса
        points = []
        for i in range(num_points):
            # Для расчета координат точек эллипса используется параметрическое уравнение эллипса
            # где параметр angle варьируется от 0 до 2pi
            # Координаты вычисляются с учетом размеров полуосей и угла его поворота
            angle = 2 * math.pi * i / num_points
            x = self.x + half_width * math.cos(angle) * cos_angle - half_height * math.sin(angle) * sin_angle
            y = self.y + half_width * math.cos(angle) * sin_angle + half_height * math.sin(angle) * cos_angle
            points.extend([x, y])
        # Рисуем эллипс
        self.canvas.create_line(points, fill="black", width=2, smooth=True, tags=self.tag)

    def move(self, dx, dy):
        self.x += dx
        self.y += dy
        self.draw_self()

    def scale(self, x, y, kx, ky):
        # Пересчет размеров
        self.x = self.x * kx + (1 - kx) * x
        self.y = self.y * ky + (1 - ky) * y
        self.width *= abs(kx)
        self.height *= abs(ky)
        if (kx < 0):
            self.angle = (180-self.angle)
        if (ky < 0):
            self.angle = (-1)*self.angle
        # Отражение относительно оси x, если необходимо
        self.draw_self()

    def rotate(self, x, y, phi):
        # Пересчет угла поворота
        self.angle += phi
        # Поворачиваем эллипс относительно точки (x, y)
        dx = self.x - x
        dy = self.y - y
        new_dx = dx * math.cos(math.radians(phi)) - dy * math.sin(math.radians(phi))
        new_dy = dx * math.sin(math.radians(phi)) + dy * math.cos(math.radians(phi))
        self.x = x + new_dx
        self.y = y + new_dy
        self.draw_self()
    


class Segment():
    def __init__(self, canvas: tk.Canvas, x1, y1, x2, y2, tag: str):
        self.x1 = x1
        self.y1 = y1
        self.x2 = x2
        self.y2 = y2
        self.canvas = canvas
        self.tag = f"segment-{tag}"
        self.draw_self()

    def draw_self(self):
        # Очищаем холст (старую версию эелмента)
        self.canvas.delete(self.tag)
        # Рисуем отрезок
        self.canvas.create_line(self.x1, self.y1, self.x2, self.y2, fill="black", width=2, tags=self.tag)

    def move(self, dx, dy):
        self.x1 += dx
        self.y1 += dy
        self.x2 += dx
        self.y2 += dy
        self.draw_self()

    def scale(self, x, y, kx, ky):
        # Пересчет координат концов отрезка
        self.x1 = x + (self.x1 - x) * kx
        self.y1 = y + (self.y1 - y) * ky
        self.x2 = x + (self.x2 - x) * kx
        self.y2 = y + (self.y2 - y) * ky
        self.draw_self()

    def rotate(self, x, y, phi):
        # Пересчет угла поворота
        angle = math.radians(phi)
        # Поворот начальной точки отрезка
        dx = self.x1 - x
        dy = self.y1 - y
        self.x1 = x + dx * math.cos(angle) - dy * math.sin(angle)
        self.y1 = y + dx * math.sin(angle) + dy * math.cos(angle)
        # Поворот конечной точки отрезка
        dx = self.x2 - x
        dy = self.y2 - y
        self.x2 = x + dx * math.cos(angle) - dy * math.sin(angle)
        self.y2 = y + dx * math.sin(angle) + dy * math.cos(angle)
        self.draw_self()