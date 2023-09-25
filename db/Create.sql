CREATE DATABASE BookDatabase;
GO

USE BookDatabase;
GO

-----------------------------------------------------------------------------
DROP TABLE Author;
GO

CREATE TABLE Author (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Name VARCHAR(255) UNIQUE NOT NULL,
  YearBirth INT NOT NULL,
  YearDeath INT,
  Country VARCHAR(255) NOT NULL,
  Genre VARCHAR(255) NOT NULL
);

BULK INSERT Author
FROM 'C:\Users\svetl\Desktop\Курсовая\db\tables\Author.csv'
WITH (DATAFILETYPE = 'widechar', FIRSTROW = 1, FIELDTERMINATOR = ';');
GO

SELECT * FROM Author;
GO
-----------------------------------------------------------------------------

-----------------------------------------------------------------------------
DROP TABLE Book;
GO

CREATE TABLE Book (
  Id INT IDENTITY(1,1),
  Name VARCHAR(255) NOT NULL,
  Genre VARCHAR(255) NOT NULL,
  Year INT NOT NULL,
  Language VARCHAR(255) NOT NULL,
  Rating FLOAT NOT NULL
);
GO

BULK INSERT Book
FROM 'C:\Users\svetl\Desktop\Курсовая\db\tables\Book.csv'
WITH (DATAFILETYPE = 'widechar', FIRSTROW = 1, FIELDTERMINATOR = ';');
GO

SELECT * FROM Book;
GO
-----------------------------------------------------------------------------

-----------------------------------------------------------------------------
DROP TABLE Series;
GO

CREATE TABLE Series (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Genre VARCHAR(255) NOT NULL,
    Publisher VARCHAR(255) NOT NULL,
    Year INT NOT NULL,
    Rating FLOAT NOT NULL
);

BULK INSERT Series
FROM 'C:\Users\svetl\Desktop\Курсовая\db\tables\Series.csv'
WITH (DATAFILETYPE = 'widechar', FIRSTROW = 1, FIELDTERMINATOR = ';');
GO

SELECT * FROM Series;
GO
-----------------------------------------------------------------------------

-----------------------------------------------------------------------------
DROP TABLE BookAuthor;
GO

CREATE TABLE BookAuthor (
  IdBook INT,
  IdAuthor INT,
  PRIMARY KEY (IdBook, IdAuthor),
  FOREIGN KEY (IdBook) REFERENCES Book (Id) ON DELETE CASCADE,
  FOREIGN KEY (IdAuthor) REFERENCES Author (Id) ON DELETE CASCADE
);

BULK INSERT BookAuthor
FROM 'C:\Users\svetl\Desktop\Курсовая\db\tables\_BookAuthor.csv'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = ';');
GO

SELECT * FROM BookAuthor;
GO
-----------------------------------------------------------------------------

-----------------------------------------------------------------------------
DROP TABLE BookSeries;
GO

CREATE TABLE BookSeries (
  IdBook INT,
  IdSeries INT,
  PRIMARY KEY (IdBook, IdSeries),
  FOREIGN KEY (IdBook) REFERENCES Book (Id) ON DELETE CASCADE,
  FOREIGN KEY (IdSeries) REFERENCES Series (Id) ON DELETE CASCADE
);

BULK INSERT BookSeries
FROM 'C:\Users\svetl\Desktop\Курсовая\db\tables\_BookSeries.csv'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = ';');
GO

SELECT * FROM BookSeries;
GO
-----------------------------------------------------------------------------

-----------------------------------------------------------------------------
DROP TABLE [User];
GO

CREATE TABLE [User] (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Login VARCHAR(255) UNIQUE NOT NULL,
  Password VARCHAR(255) NOT NULL,
  Permission VARCHAR(255) NOT NULL,
);


SELECT * FROM [User];
GO

UPDATE [User]
SET Permission = 'admin'
WHERE id = 2;
-----------------------------------------------------------------------------

-----------------------------------------------------------------------------
DROP TABLE Bookshelf;
GO

CREATE TABLE Bookshelf (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  IdUser INT NOT NULL,
  Number INT,
  Rating FLOAT,
);

ALTER TABLE Bookshelf
ADD FOREIGN KEY (IdUSer) REFERENCES [User](Id) ON DELETE CASCADE;


SELECT * FROM Bookshelf;
GO
-----------------------------------------------------------------------------

-----------------------------------------------------------------------------
DROP TABLE BookshelfBook;
GO

CREATE TABLE BookshelfBook (
  IdBookshelf INT,
  IdBook INT,
  IsRead BIT,
  PRIMARY KEY (IdBookshelf, IdBook),
  FOREIGN KEY (IdBookshelf) REFERENCES Bookshelf (Id) ON DELETE CASCADE,
  FOREIGN KEY (IdBook) REFERENCES Book (Id) ON DELETE CASCADE
);


SELECT * FROM BookshelfBook;
GO
-----------------------------------------------------------------------------