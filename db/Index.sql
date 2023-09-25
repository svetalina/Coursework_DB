USE BookDatabase;
GO

DROP TABLE NewBook;
GO

SELECT TOP 5000 *
INTO NewBook
FROM Book;
GO

CREATE CLUSTERED INDEX book_rating_ind ON NewBook(Rating);
GO

CREATE NONCLUSTERED INDEX book_rating_ind ON NewBook(Rating);
GO

SELECT * FROM NewBook ORDER BY Rating;

