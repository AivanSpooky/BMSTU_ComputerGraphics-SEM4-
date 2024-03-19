from tkinter import Tk, Listbox
import tkinter as tk
from tkinter.ttk import *
from errors import *
from triangle import *

err = "err"
#region Menu Funcs
def show_info(root: Tk):
    info = """
    Лабораторная работа №1\n
    по дисциплине "Компьютерная графика"\n
    Выполнил: Смирнов Иван Владимирович (ИУ7-42Б)\n
    \n\n\n
    (вариант 27)\n
    Задача: На плоскости дано множество точек. Найти такой треугольник с вершинами в этих точках, 
    у которого разность максимального и минимального количества точек, попавших в каждый из 6-ти 
    треугольников, образованных пересечением медиан, максимальна.
    """
    info_window = tk.Toplevel(root)
    info_label = tk.Label(info_window, text=info)
    info_label.pack()
#endregion

#region Update Funcs
def update_points(xbox: Listbox, ybox: Listbox, points: list):
    for i in range(len(points)):
        points[i][0] = i + 1

        if i < xbox.size():
            x_value = xbox.get(i)
            if points[i][1] != x_value:
                points[i][1] = x_value

        if i < ybox.size():
            y_value = ybox.get(i)
            if points[i][2] != y_value:
                points[i][2] = y_value

def update_point_numbers(numbox, points):
    numbox.delete(0, tk.END)
    for i in range(1, len(points) + 1):
        numbox.insert(tk.END, str(i))
#endregion

#region Edit Funcs
def edit_item(event, listbox: Listbox, root: Tk, points: list, xbox, ybox):
    selected_index = listbox.curselection()
    if selected_index:
        index = selected_index[0]
        value = listbox.get(index)
        edit_window = tk.Toplevel(root)
        edit_window.resizable(False, False)
        edit_window.title("Редактирование")
        edit_entry = tk.Entry(edit_window)
        edit_entry.insert(0, value)
        edit_entry.pack(padx=10, pady=10)
        save_button = tk.Button(edit_window, text="Сохранить", command=lambda: save_edit(index, edit_entry.get(), edit_window, listbox, points, xbox, ybox))
        save_button.pack()

def save_edit(index: int, new_value, window, listbox: Listbox, points: list, xbox, ybox):
    value = check_float_value(new_value)
    if (value != err):
        listbox.delete(index)
        listbox.insert(index, value)
        update_points(xbox, ybox, points)
        window.destroy()
    else:
        error_wrong_content()
#endregion
    
#region Entry Checks
def check_float_value(content: str):
    try:
        value = float(content)
        return value
    except Exception:
        return err
    
def check_float_values(x_content: str, y_content: str):
    try:
        x = float(x_content)
        y = float(y_content)
        return x, y
    except Exception:
        return err, err
#endregion

#region Button Funcs
def add_item(numbox: Listbox, xbox: Listbox, ybox: Listbox, item: list, points: list):
    numbox.insert(tk.END, item[0])
    xbox.insert(tk.END, item[1])
    ybox.insert(tk.END, item[2])
    points.append(item)

def add_new_item(numbox: Listbox, xbox: Listbox, ybox: Listbox, x_entry: Entry, y_entry: Entry, points: list):
    x_content = x_entry.get()
    y_content = y_entry.get()
    x, y = check_float_values(x_content, y_content)
    if (x != err and y != err):
        item = [len(points)+1, x, y]
        add_item(numbox, xbox, ybox, item, points)
    else:
        error_wrong_content()

def delete_all(numbox: Listbox, xbox: Listbox, ybox: Listbox, points: list, canvas, info_label: Label, info_maxlabel: Label, info_minlabel: Label, inf_label: Label):
    canvas.delete("all")
    numbox.delete(0, tk.END)
    xbox.delete(0, tk.END) 
    ybox.delete(0, tk.END)
    info_label.config(text=f"Максимальная разница:")
    info_minlabel.config(text=f"Минимальное кол-во внутренних вершин (из 6 под-треугольников):")
    info_maxlabel.config(text=f"Максимальное кол-во внутренних вершин (из 6 под-треугольников):")
    inf_label.config(text=f"Треугольник построен на точках:")
    points.clear()

def delete_point(numbox, xbox, ybox, points, index):
    del points[index]
    numbox.delete(index)
    xbox.delete(index)
    ybox.delete(index)
    update_points(xbox, ybox, points)
    update_point_numbers(numbox, points)
#endregion

#region Solve Funcs
def draw_points(canvas, points):
    canvas.delete("all")
    canvas_width = canvas.winfo_width() - 20  # Отступ
    canvas_height = canvas.winfo_height() - 20  # Отступ
    
    max_x = max(abs(point[1]) for point in points)  # Используем модуль для масштаба с отображением отрицательных координат
    max_y = max(abs(point[2]) for point in points)  # Используем модуль для масштаба с отображением отрицательных координат
    
    scale_x = canvas_width / (2 * max_x)  # Масштабируем для положительных и отрицательных координат
    scale_y = canvas_height / (2 * max_y)  # Масштабируем для положительных и отрицательных координат
    
    scale = min(scale_x, scale_y)
    
    for point in points:
        x = point[1] * scale + canvas_width / 2
        y = canvas_height / 2 - point[2] * scale  # Изменяем знак для отображения отрицательных координат
        canvas.create_oval(x - 5, y - 5 + 10, x + 5, y + 5 + 10, fill="black")  # Добавляем 10 для отступа сверху
        canvas.create_text(x-15, y + 10, text=str(point[0]))
    
def solve(canvas, points, info_label: Label, info_maxlabel: Label, info_minlabel: Label, inf_label: Label):
    if (len(points) > 0):
        triangles = create_triangles_from_points(points)
        if (len(triangles) > 0):
            max_triangle, max_property, a, b = find_maximum_task_property(triangles)
            info_label.config(text=f"Максимальная разница: {max_triangle.draw_triangle(canvas=canvas)}")
            info_maxlabel.config(text=f"Максимальное кол-во внутренних вершин (из 6 под-треугольников): {len(a.inner_points)}")
            info_minlabel.config(text=f"Минимальное кол-во внутренних вершин (из 6 под-треугольников): {len(b.inner_points)}")
            inf_label.config(text=f"Треугольник построен на точках с номерами: ({max_triangle.vertices[0][0]}) ({max_triangle.vertices[1][0]}) ({max_triangle.vertices[2][0]})")
        else:
            error_no_triangles()
    else:
        error_no_points()
#endregion

#region DEBUG Funcs
def debug_print():
    print("btn clicked")

def debug_print_points(points: list):
    print(points)
#endregion