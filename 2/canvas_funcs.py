import tkinter as tk
from errors import *
from geometry import *
from enum import Enum

class Cat():
    def __init__(self, canvas, center_x, center_y):
        self.canvas = canvas
        body = Ellipse(canvas, center_x, center_y, 50, 100, "body")
        head = Ellipse(canvas, center_x, center_y-body.height/2-body.width/2, 50, 50, "head")
        x_eye_offset = 10
        eye1 = Ellipse(canvas, center_x-x_eye_offset, center_y-body.height/2-body.width/2-15, 10, 10, "eye1")
        eye2 = Ellipse(canvas, center_x+x_eye_offset, center_y-body.height/2-body.width/2-15, 10, 10, "eye2")
        x_mustache_offset = 30
        y_mustache_offset = 15
        m1 = Segment(canvas, center_x-x_mustache_offset, center_y-body.height/2-body.width/2, center_x+x_mustache_offset, center_y-body.height/2-body.width/2, "mustache-1")
        m2 = Segment(canvas, center_x-x_mustache_offset, center_y-body.height/2-body.width+y_mustache_offset, center_x+x_mustache_offset, center_y-body.height/2-y_mustache_offset, "mustache-2")
        m3 = Segment(canvas, center_x-x_mustache_offset, center_y-body.height/2-y_mustache_offset, center_x+x_mustache_offset, center_y-body.height/2-body.width+y_mustache_offset, "mustache-3")

        le1 = Segment(canvas, center_x-head.width/2, center_y-body.height/2-3*head.width/4, center_x-3*head.width/8, center_y-body.height/2-5*head.width/4, "left-ear-1")
        le2 = Segment(canvas, center_x-3*head.width/8, center_y-body.height/2-5*head.width/4, center_x, center_y-body.height/2-head.height, "left-ear-2")

        re1 = Segment(canvas, center_x+head.width/2, center_y-body.height/2-3*head.width/4, center_x+3*head.width/8, center_y-body.height/2-5*head.width/4, "right-ear-1")
        re2 = Segment(canvas, center_x+3*head.width/8, center_y-body.height/2-5*head.width/4, center_x, center_y-body.height/2-head.height, "right-ear-2")

        self.elems = [body, head, eye1, eye2, m1, m2, m3, le1, le2, re1, re2]
    
    def draw_elems(self):
        self.canvas.delete("all")
        for elem in self.elems:
            elem.draw_self()

    def move_elems(self, dx, dy):
        for elem in self.elems:
            elem.move(dx, dy)
        self.draw_elems()

    def scale_elems(self, x, y, kx, ky):
        for elem in self.elems:
            elem.scale(x, y, kx, ky)
        self.draw_elems()

    def rotate_elems(self, x, y, angle):
        for elem in self.elems:
            elem.rotate(x, y, angle)
        self.draw_elems()

    def update_label(self, label: tk.Label):
        label.config(text=f"Координаты центра кота: x={round(self.elems[0].x, 6)}, y={round(self.elems[0].y, 6)}")

class Action(Enum):
    NO_ACTION = 0
    MOVE = 1
    SCALE = 2
    ROTATE = 3

class ActionManager():
    def __init__(self, cat: Cat):
        self.action = Action.NO_ACTION
        self.params = None
        self.action_set = False
        self.cat = cat

    def set_action(self, action, params):
        self.action = action
        self.action_set = True
        self.params = params

    def undo_action(self, cat: Cat, label):
        if (self.action_set == False):
            ErrorHandler(PreviousActionNotExist()).handle()
        elif (self.action == Action.NO_ACTION):
            ErrorHandler(TryToUndoSecondAction()).handle()
        else:
            if (self.action == Action.MOVE):
                self.cat.move_elems((-1)*self.params[0], (-1)*self.params[1])
            elif (self.action == Action.SCALE):
                self.cat.scale_elems(self.params[0], self.params[1], 1/self.params[2], 1/self.params[3])
            else:
                self.cat.rotate_elems(self.params[0], self.params[1], (-1)*self.params[2])
            self.action = Action.NO_ACTION
            self.params = None
            cat.update_label(label)

def show_info(root: tk.Tk):
    info = """
    Лабораторная работа №2\n
    по дисциплине "Компьютерная графика"\n
    Выполнил: Смирнов Иван Владимирович (ИУ7-42Б)\n
    \n\n\n
    (вариант 17)\n
    Задача: На плоскости по центру нарисован исходный рисунок. Осуществить его перенос, масштабирование и поворот.
    """
    info_window = tk.Toplevel(root)
    info_label = tk.Label(info_window, text=info)
    info_label.pack()

def move_func(cat: Cat, action: ActionManager, center_cat_label: tk.Label, dx_entry, dy_entry):
    try:
        x_content = dx_entry.get()
        y_content = dy_entry.get()
        if (len(str(x_content)) == 0 or len(str(y_content)) == 0):
            ErrorHandler(NothingEntered()).handle()
            return
        dx = float(x_content)
        dy = float(y_content)
        action.set_action(Action.MOVE, [dx, dy])
        cat.move_elems(dx, dy)
        cat.update_label(center_cat_label)
    except Exception:
        ErrorHandler(FloatNotEntered()).handle()

def scale_func(cat: Cat, action: ActionManager, center_cat_label: tk.Label, Mx_entry, My_entry, kx_entry, ky_entry):
    try:
        Mx_content = Mx_entry.get()
        My_content = My_entry.get()
        kx_content = kx_entry.get()
        ky_content = ky_entry.get()
        if (len(str(Mx_content)) == 0 or len(str(My_content)) == 0 or len(str(kx_content)) == 0 or len(str(ky_content)) == 0):
            ErrorHandler(NothingEntered()).handle()
            return
        Mx = float(Mx_content)
        My = float(My_content)
        kx = float(kx_content)
        ky = float(ky_content)
        if (kx == 0 or ky == 0):
            ErrorHandler(WrongScale()).handle() 
        else:
            action.set_action(Action.SCALE, [Mx, My, kx, ky])
            cat.scale_elems(Mx, My, kx, ky)
            cat.update_label(center_cat_label)
    except Exception:
        ErrorHandler(FloatNotEntered()).handle()

def rotate_func(cat: Cat, action: ActionManager, center_cat_label: tk.Label, Cx_entry, Cy_entry, angle_entry):
    try:
        Cx_content = Cx_entry.get()
        Cy_content = Cy_entry.get()
        angle_content = angle_entry.get()
        if (len(str(Cx_content)) == 0 or len(str(Cy_content)) == 0 or len(str(angle_content)) == 0):
            ErrorHandler(NothingEntered()).handle()
            return
        Cx = float(Cx_content)
        Cy = float(Cy_content)
        angle = float(angle_content)
        action.set_action(Action.ROTATE, [Cx, Cy, angle])
        cat.rotate_elems(Cx, Cy, angle)
        cat.update_label(center_cat_label)
    except Exception:
        ErrorHandler(FloatNotEntered()).handle()