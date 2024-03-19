from tkinter import Tk, Listbox
import tkinter as tk
from tkinter.ttk import *
from listbox import *
from triangle import *

root = Tk()

#region Listbox funcs
def on_numbox_select(event):
    selected_index = numbox.curselection()
    if selected_index:
        index = selected_index[0]
        delete_btn.config(command=lambda: delete_point(numbox, xbox, ybox, points, index))

def on_scroll(*args):
    numbox.yview(*args)
    xbox.yview(*args)
    ybox.yview(*args)
#endregion

#region: root settings
global_entry_width = 24

root.geometry("1920x1080")
root.title("Лабораторная работа №1")
root['bg'] = 'gray'
#endregion

#region: X and Y labels & entries
x_label = Label(root, text = "X", borderwidth=2, relief="groove")
x_label.grid(row=0, column=0, sticky="EW", columnspan=3)

y_label = Label(root, text = "Y", borderwidth=2, relief="groove")
y_label.grid(row=0, column=3, sticky="EW", columnspan=3)

x_entry = Entry(root, width=global_entry_width)
x_entry.grid(row=1, column=0, sticky="EW", columnspan=3)

y_entry = Entry(root, width=global_entry_width)
y_entry.grid(row=1, column=3, sticky="EW", columnspan=3)
#endregion

points = []

#region: Point Listbox labels, data and scrollbars 
lb_num_label = Label(root, text = "#", foreground="white", background="black")
lb_num_label.grid(row=2, column=0, sticky="EW", columnspan=2)
lb_x_label = Label(root, text = "X", foreground="white", background="black")
lb_x_label.grid(row=2, column=2, sticky="EW", columnspan=2)
lb_y_label = Label(root, text = "Y", foreground="white", background="black")
lb_y_label.grid(row=2, column=4, sticky="EW", columnspan=2)

numbox = Listbox(root, width=int(global_entry_width/3*2), height=15, font=("Times New Roman", 14))
numbox.grid(row=3, column=0, sticky="EW", columnspan=2)
numbox.bind("<<ListboxSelect>>", on_numbox_select)

xbox = Listbox(root, width=int(global_entry_width/3*2), height=15, font=("Times New Roman", 14), selectmode=tk.SINGLE)
xbox.grid(row=3, column=2, sticky="EW", columnspan=2)
xbox.bind("<Double-Button-1>", lambda event: edit_item(event, xbox, root, points, xbox, ybox))

ybox = Listbox(root, width=int(global_entry_width/3*2), height=15, font=("Times New Roman", 14), selectmode=tk.SINGLE)
ybox.grid(row=3, column=4, sticky="EW", columnspan=2)
ybox.bind("<Double-Button-1>", lambda event: edit_item(event, ybox, root, points, xbox, ybox))


scrollbar = Scrollbar(root, command=on_scroll)
scrollbar.grid(row=2, column=6, sticky="NSEW", rowspan=2)

numbox.config(yscrollcommand=scrollbar.set)
xbox.config(yscrollcommand=scrollbar.set)
ybox.config(yscrollcommand=scrollbar.set)

#endregion

#region canvas & info label
canvas = tk.Canvas(root, width=850, height=850)
canvas.grid(row=1, column=8, rowspan=30, sticky="NSEW")


info_maxlabel = tk.Label(root, text=f"Максимальное кол-во внутренних вершин (из 6 под-треугольников):", bg="darkorchid1", font=("Aerial", 10))
info_maxlabel.grid(row=32, column=8, sticky="NSEW")
info_minlabel = tk.Label(root, text=f"Минимальное кол-во внутренних вершин (из 6 под-треугольников):", bg="darkorange", font=("Aerial", 10))
info_minlabel.grid(row=33, column=8, sticky="NSEW")
info_label = tk.Label(root, text=f"Максимальная разница:", font=("Aerial", 10))
info_label.grid(row=34, column=8, sticky="NSEW")
inf_label = tk.Label(root, text=f"Треугольник построен на точках:", font=("Aerial", 10))
inf_label.grid(row=31, column=8, sticky="NSEW")
#endregion

#region BUTTONS
create_btn = tk.Button(root, text="Добавить точку", command=lambda: add_new_item(numbox, xbox, ybox, x_entry, y_entry, points))
create_btn.grid(row=0, column=6, sticky="EW")
delete_btn = tk.Button(root, text="Удалить точку", command=lambda: None)
delete_btn.grid(row=0, column=7, sticky="EW")
delall_btn = tk.Button(root, text="Удалить все", command=lambda: delete_all(numbox, xbox, ybox, points, canvas, info_label, info_maxlabel, info_minlabel, inf_label))
delall_btn.grid(row=1, column=6, sticky="EW")

solve_btn = tk.Button(root, text="Решить", command=lambda: solve(canvas, points, info_label, info_maxlabel, info_minlabel, inf_label))
solve_btn.grid(row=1, column=7, sticky="EW")
#endregion

#region MENU
menu_bar = tk.Menu(root)

info_menu = tk.Menu(menu_bar, tearoff=0)
info_menu.add_command(label="Информация о задании", command=lambda: show_info(root))

menu_bar.add_cascade(label="Меню", menu=info_menu)

root.config(menu=menu_bar)
#endregion

root.mainloop()