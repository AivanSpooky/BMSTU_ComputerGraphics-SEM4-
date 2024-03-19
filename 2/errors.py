import tkinter as tk
from tkinter import messagebox

class CustomError():
    def __init__(self, msg):
        self.msg = msg

class PreviousActionNotExist(CustomError):
    def __init__(self, message="Невозможно отменить несуществующее действие!"):
        super().__init__(message)

class TryToUndoSecondAction(CustomError):
    def __init__(self, message="Нельзя отменить больше 1 действия!"):
        super().__init__(message)

class NothingEntered(CustomError):
    def __init__(self, message="Пожалуйста, заполните недостающие поля!"):
        super().__init__(message)

class FloatNotEntered(CustomError):
    def __init__(self, message="Пожалуйста, введите вещественное число!"):
        super().__init__(message)

class WrongScale(CustomError):
    def __init__(self, message="Пожалуйста, введите коэффициенты масштабирования, отличные от нуля!"):
        super().__init__(message)

class ErrorHandler():
    def __init__(self, ce: CustomError):
        self.ce = ce
        
    def handle(self):
        messagebox.showerror("Ошибка", self.ce.msg)

