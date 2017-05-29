CREATE DATABASE Users
GO

USE Users
GO

CREATE TABLE Question
(
Question_id int IDENTITY(1, 1) CONSTRAINT PK_QUESTION_ID PRIMARY KEY,
Question NVARCHAR(200) NOT NULL UNIQUE
)
GO

CREATE TABLE Account
(
[Login] NVARCHAR(30) CONSTRAINT PK_LOGIN PRIMARY KEY,
[Password] NVARCHAR(30) NOT NULL,
Secret_question_id int NOT NULL CONSTRAINT FK_SQID FOREIGN KEY REFERENCES Question(Question_id),
Answer NVARCHAR(200) NOT NULL
)
GO

CREATE TABLE Lecturer
(
Lecturer_id int IDENTITY(1, 1) CONSTRAINT PK_LECTURER_ID PRIMARY KEY,
Name NVARCHAR(30) NOT NULL,
Surname NVARCHAR(30) NOT NULL,
Patronymic NVARCHAR(30) NOT NULL,
[Login] NVARCHAR(30) CONSTRAINT FK_LOGIN_L FOREIGN KEY REFERENCES Account([Login])
)
GO

CREATE TABLE Student
(
Student_id int IDENTITY(1, 1) CONSTRAINT PK_STUDENT_ID PRIMARY KEY,
Name NVARCHAR(30) NOT NULL,
Surname NVARCHAR(30) NOT NULL,
Patronymic NVARCHAR(30) NOT NULL,
[Group] int CHECK([Group] BETWEEN 1 AND 10),
[Login] NVARCHAR(30) NOT NULL CONSTRAINT FK_LOGIN_S FOREIGN KEY REFERENCES Account([Login])
)
GO

CREATE TABLE Categories
(
Cat_id INT IDENTITY(1, 1) PRIMARY KEY,
Cat_name NVARCHAR(100)
)
GO

CREATE TABLE CatQuestions
(
CatQuestionID INT IDENTITY(1, 1) PRIMARY KEY,
QDesc TEXT NOT NULL,
QCat_id INT FOREIGN KEY REFERENCES Categories(Cat_id),
QAnswer NVARCHAR(1) CHECK(QAnswer IN ('A', 'B', 'C', 'D')) NOT NULL,
QWeight INT NOT NULL CHECK(QWeight BETWEEN 4 AND 10)
)
GO

CREATE TABLE CatAnswers
(
Answers_id INT IDENTITY(1, 1) PRIMARY KEY,
AnswerA TEXT NOT NULL,
AnswerB TEXT NOT NULL,
AnswerC TEXT NOT NULL,
AnswerD TEXT NOT NULL,
ACat_id INT FOREIGN KEY REFERENCES CatQuestions(CatQuestionID)
)
GO

CREATE TABLE Results
(
Results_id INT IDENTITY(1, 1) PRIMARY KEY,
[Owner] NVARCHAR(30) FOREIGN KEY REFERENCES Account([Login]),
Points INT,
TotalPoints INT,
Mark INT,
RCat_id INT FOREIGN KEY REFERENCES Categories(Cat_id)
)
GO

INSERT INTO Question(Question)
VALUES
('Девичья фамилия матери'),
('Кличка первого домашнего животного'),
('Ваша любимая игра'),
('Ваше прозвище в детсве'),
('Марка вашей первой машины'),
('Отчество вашей бабушки'),
('Герой вашего дества'),
('Любимое женское/мужское имя'),
('Город вашего ближайшего родственника'),
('Имя любимого музыкального исполнителя')
GO

INSERT INTO Results([Owner], Points, TotalPoints, Mark, RCat_id)
VALUES
('123', 60, 70, 9, 2),
('123', 50, 50, 10, 2),
('123', 60, 60, 10, 2)

GO

INSERT INTO Categories(Cat_name)
VALUES
('.NET Платформа'),
('Основы C#'),
('Классы в C#'),
('Наследоание и интерфейсы'),
('Делегаты и события'),
('Исключения'),
('Обобщенные коллекции и LINQ'),
('WinForms'),
('Наследоание и интерфейсы'),
('XML. LINQ to XML'),
('Регулярные выражения'),
('WPF')
GO

INSERT INTO CatQuestions(QDesc, QCat_id, QAnswer, QWeight)
VALUES
('Назовите класс .NET, от которого наследуются все классы.', 1, 'B', 4),
('Какие типы относятся к типам-значениям?', 1, 'A', 6),
('Как выполнить консольный ввод/вывод?', 1, 'C', 4),
('В чем различие между классом и структурой?', 1, 'A', 5),
('Что такое анонимный тип в C#?', 1, 'D', 5),
('В чем разница между равенством и тождеством объектов?', 1, 'A', 8)

INSERT INTO CatAnswers (AnswerA, AnswerB, AnswerC, AnswerD, ACat_id)
VALUES
('a1', 'a2', 'a3', 'a4', 1),
('b1', 'b2', 'b3', 'b4', 2),
('c1', 'c2', 'c3', 'c4', 3),
('d1', 'd2', 'd3', 'd4', 4),
('e1', 'e2', 'e3', 'e4', 5),
('f1', 'f2', 'f3', 'f4', 6)

DELETE CatAnswers
DELETE Results
DELETE CatQuestions
DELETE Categories
DROP TABLE CatAnswers
DROP TABLE Results
DROP TABLE CatQuestions
DROP TABLE CatAnswers
DROP DATABASE Users