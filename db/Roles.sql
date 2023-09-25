USE BookDatabase;
GO

----------------------------------------------------------------
DROP LOGIN [guest];
DROP USER [guest];

CREATE LOGIN [guest] WITH PASSWORD = 'guest';
CREATE USER [guest] FOR LOGIN [guest];

GRANT SELECT ON Author (Name, Genre, Country) TO [guest];
GRANT SELECT ON Book (Name, Genre, Rating) TO [guest];
GRANT SELECT ON Series (Name, Genre, Rating) TO [guest];

GRANT SELECT, INSERT ON [User] TO [guest];

GRANT SELECT, INSERT ON Bookshelf TO [guest];


EXECUTE AS USER = 'guest';
REVERT;
----------------------------------------------------------------

----------------------------------------------------------------
DROP LOGIN [user];
DROP USER [user];

CREATE LOGIN [user] WITH PASSWORD = 'user';
CREATE USER [user] FOR LOGIN [user];

GRANT SELECT ON Author TO [user];
GRANT SELECT ON Book TO [user];
GRANT SELECT ON Series TO [user];

GRANT SELECT ON BookAuthor TO [user];
GRANT SELECT ON BookSeries TO [user];

GRANT SELECT, INSERT, UPDATE, DELETE  ON [User] TO [user];

GRANT SELECT, INSERT, UPDATE, DELETE ON Bookshelf TO [user];

GRANT SELECT, INSERT, UPDATE, DELETE ON BookshelfBook TO [user];


EXECUTE AS USER = 'user';
REVERT;
----------------------------------------------------------------

----------------------------------------------------------------
DROP LOGIN [admin];
DROP USER [admin];

CREATE LOGIN [admin] WITH PASSWORD = 'admin';
CREATE USER [admin] FOR LOGIN [admin];
EXEC sp_addrolemember 'db_owner', 'admin';


EXECUTE AS USER = 'admin';
REVERT;
----------------------------------------------------------------
