import tkinter as tk
from geometry import *
from canvas_funcs import *

# Создаем окно
root = tk.Tk()
root.title("Лабораторная работа №2")
root.geometry("1280x800")

# Создаем Canvas
width = height = 690

canvas = tk.Canvas(root, width=width, height=height, highlightthickness=2, highlightbackground="black")
canvas.grid(row=1, column=4, sticky="NSEW", rowspan=10)
center_x = width/2
center_y = height/2
center_label = tk.Label(root, text=f"Координаты центра поля: x={center_x}, y={center_y}", font=("Aerial", 12, "italic"))
center_label.grid(row=12, column=4, sticky="NSEW")

# Рисуем все элементы кота
cat = Cat(canvas, center_x, center_y)
cat.draw_elems()
action = ActionManager(cat)
center_cat_label = tk.Label(root, text=f"Координаты центра кота: x={center_x}, y={center_y}", font=("Aerial", 12, "italic"))
center_cat_label.grid(row=13, column=4, sticky="NSEW")


move_label = tk.Label(root, text=f"Перенос:", font=("Aerial", 10))
move_label.grid(row=0, column=0, sticky="NSEW", columnspan=4)
dx_entry = tk.Entry(root)
dx_entry.grid(row=1, column=0, sticky="NSEW", columnspan=2)
dy_entry = tk.Entry(root)
dy_entry.grid(row=1, column=2, sticky="NSEW", columnspan=2)
move_btn = tk.Button(root, text="Переместить", command=lambda: move_func(cat, action, center_cat_label, dx_entry, dy_entry))
move_btn.grid(row=2, column=0, sticky="NSEW", columnspan=4)

scale_label = tk.Label(root, text=f"Масштабирование:", font=("Aerial", 10))
scale_label.grid(row=3, column=0, sticky="S", columnspan=4)
Mx_label = tk.Label(root, text=f"X т. масштабирования:", font=("Aerial", 10))
Mx_label.grid(row=4, column=0, sticky="NSEW")
My_label = tk.Label(root, text=f"Y т. масштабирования:", font=("Aerial", 10))
My_label.grid(row=4, column=1, sticky="NSEW")
kx_label = tk.Label(root, text=f"Коэф kx:", font=("Aerial", 10))
kx_label.grid(row=4, column=2, sticky="NSEW")
ky_label = tk.Label(root, text=f"Коэф ky:", font=("Aerial", 10))
ky_label.grid(row=4, column=3, sticky="NSEW")

Mx_entry = tk.Entry(root, font=("Aerial", 10))
Mx_entry.grid(row=5, column=0, sticky="NSEW")
My_entry = tk.Entry(root, font=("Aerial", 10))
My_entry.grid(row=5, column=1, sticky="NSEW")
kx_entry = tk.Entry(root, font=("Aerial", 10))
kx_entry.grid(row=5, column=2, sticky="NSEW")
ky_entry = tk.Entry(root, font=("Aerial", 10))
ky_entry.grid(row=5, column=3, sticky="NSEW")
scale_btn = tk.Button(root, text="Масштабировать", command=lambda: scale_func(cat, action, center_cat_label, Mx_entry, My_entry, kx_entry, ky_entry))
scale_btn.grid(row=6, column=0, sticky="NSEW", columnspan=4)


rotate_label = tk.Label(root, text=f"Поворот:", font=("Aerial", 10))
rotate_label.grid(row=7, column=0, sticky="S", columnspan=4)
Cx_label = tk.Label(root, text=f"X т. центра поворота:", font=("Aerial", 10))
Cx_label.grid(row=8, column=0, sticky="NSEW")
Cy_label = tk.Label(root, text=f"Y т. центра поворота:", font=("Aerial", 10))
Cy_label.grid(row=8, column=1, sticky="NSEW")
angle_label = tk.Label(root, text=f"Угол поворота:", font=("Aerial", 10))
angle_label.grid(row=8, column=2, sticky="NSEW", columnspan=2)

Cx_entry = tk.Entry(root, font=("Aerial", 10))
Cx_entry.grid(row=9, column=0, sticky="NSEW")
Cy_entry = tk.Entry(root, font=("Aerial", 10))
Cy_entry.grid(row=9, column=1, sticky="NSEW")
angle_entry = tk.Entry(root, font=("Aerial", 10))
angle_entry.grid(row=9, column=2, sticky="NSEW", columnspan=2)

rotate_btn = tk.Button(root, text="Повернуть", command=lambda: rotate_func(cat, action, center_cat_label, Cx_entry, Cy_entry, angle_entry))
rotate_btn.grid(row=10, column=0, sticky="NSEW", columnspan=4)

undo_btn = tk.Button(root, text="Отменить последнее действие", command=lambda: action.undo_action(cat, center_cat_label))
undo_btn.grid(row=11, column=0, sticky="NSEW", columnspan=4)

#region MENU
menu_bar = tk.Menu(root)

info_menu = tk.Menu(menu_bar, tearoff=0)
info_menu.add_command(label="Информация о задании", command=lambda: show_info(root))

menu_bar.add_cascade(label="Меню", menu=info_menu)

root.config(menu=menu_bar)
#endregion

root.mainloop()