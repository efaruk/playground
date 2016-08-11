CREATE TABLE Account1
(
    Id INTEGER NOT NULL,
    FullName TEXT NOT NULL,
    UserName TEXT NOT NULL,
    Password TEXT,
    Token TEXT,
    IsLocked INTEGER DEFAULT 0,
    IsAdmin INTEGER DEFAULT 0
);
INSERT INTO Account1(Id, FullName, UserName, Password, Token, IsLocked) SELECT Id, FullName, UserName, Password, Token, IsLocked FROM Account;

DROP TABLE Account;
ALTER TABLE Account1 RENAME TO Account;

CREATE UNIQUE INDEX Account_Token_uindex ON Account (Token);
CREATE UNIQUE INDEX Account_UserName_uindex ON Account (UserName);
CREATE UNIQUE INDEX Account_Id_uindex ON Account (Id);