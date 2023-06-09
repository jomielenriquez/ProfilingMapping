CREATE TABLE TBL_REQUEST(
	REQUESTID UNIQUEIDENTIFIER NOT NULL,
	NAMEID UNIQUEIDENTIFIER NOT NULL,
	COMMENT NVARCHAR(500) NULL,
	CREATEDDATE DATETIME NOT NULL,
	CREATEDBY NVARCHAR(50) NULL,
	UPDATEDDATE DATETIME NULL,
	UPDATEDBY NVARCHAR(50) NULL,
	CONSTRAINT PK_TBL_REQUEST_REQUESTID PRIMARY KEY NONCLUSTERED(
		REQUESTID ASC
	)ON [PRIMARY],
)ON [PRIMARY]


ALTER TABLE TBL_REQUEST ADD CONSTRAINT DF_TBL_REQUEST_REQUESTID DEFAULT (NEWID()) FOR REQUESTID
ALTER TABLE TBL_REQUEST ADD CONSTRAINT DF_TBL_REQUEST_CREATEDDATE DEFAULT (GETDATE()) FOR CREATEDDATE
ALTER TABLE TBL_REQUEST ADD CONSTRAINT FK_TBL_REQUEST_NAMEID FOREIGN KEY (NAMEID) REFERENCES TBL_NAMES(NAMEID)