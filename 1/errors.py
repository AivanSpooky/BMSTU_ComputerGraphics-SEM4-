import tkinter as tk
from tkinter import messagebox

def error_wrong_content():
    messagebox.showerror("Ошибка", "Некорректный ввод. Пожалуйста, введите действительные числа.")

def error_no_points():
    messagebox.showerror("Ошибка", "Недостаточно указано точек.")

def error_no_triangles():
    messagebox.showerror("Ошибка", "Не удалось построить ни один треугольник (все треугольники вырожденные).")
