USE BookDatabase;
GO


DROP TRIGGER UpdateNumberBookshelf, UpdateRatingBookshelf;
GO


CREATE TRIGGER UpdateNumberBookshelf
ON Bookshelfbook
AFTER INSERT, DELETE
AS
BEGIN
  UPDATE bs
  SET Number = Number + 1
  FROM Bookshelf bs
  INNER JOIN inserted ins ON bs.Id = ins.IdBookshelf;

  UPDATE bs
  SET Number = Number - 1
  FROM Bookshelf bs
  INNER JOIN deleted del ON bs.Id = del.IdBookshelf;
END;
GO


CREATE TRIGGER UpdateRatingBookshelf
ON BookshelfBook
AFTER INSERT, DELETE
AS
BEGIN
  UPDATE Bookshelf
  SET Rating = ROUND((
    SELECT COALESCE(AVG(b.Rating), 0)
    FROM BookshelfBook bb
    INNER JOIN Book b ON bb.IdBook = b.Id
    WHERE bb.IdBookshelf = Bookshelf.Id
  ), 2)
  FROM Bookshelf
  INNER JOIN (
    SELECT IdBookshelf FROM inserted
    UNION
    SELECT IdBookshelf FROM deleted
  ) insdel ON Bookshelf.Id = insdel.IdBookshelf;
END;
GO