USE AuftragsverwaltungHistory;

select * from dbo.Articles;
delete dbo.Articles;

ALTER TABLE dbo.Articles SET (SYSTEM_VERSIONING = OFF);
GO
ALTER TABLE dbo.Articles DROP PERIOD FOR SYSTEM_TIME;
GO
INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	8,
	'14.50',
    'Sonnen T-Shirt',
    '2018-01-02 12:00:00',
    '2020-04-05 00:00:00'
);

INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	2,
	'39.50',
    'Sneaker',
    '2018-03-13 12:00:00',
    '2020-04-05 00:00:00'
);

INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	8,
	'19.50',
    'Mond T-Shirt',
    '2018-04-17 12:00:00',
    '2020-04-05 00:00:00'
);

INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	8,
	'9.50',
    'Sterne T-Shirt',
    '2018-07-21 12:00:00',
    '2020-04-05 00:00:00'
);

INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	6,
	'29.90',
    'Tiger Pantoffeln',
    '2018-12-21 12:00:00',
    '2020-04-05 00:00:00'
);

INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	7,
	'85.50',
    'Braune Wanderschuhe',
    '2019-01-30 12:00:00',
    '2020-04-05 00:00:00'
);

INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	1,
	'99.90',
    'Abendkleid',
    '2019-06-30 12:00:00',
    '2020-04-05 00:00:00'
);

INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	11,
	'29.00',
    'Gelbe kurze Hose',
    '2019-09-19 12:00:00',
    '2020-04-05 00:00:00'
);

INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	5,
	'15.50',
    'Braune Sandalen',
    '2019-11-14 12:00:00',
    '2020-04-05 00:00:00'
);

INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	9,
	'13.50',
    'Wolf Pullover',
    '2020-04-11 12:00:00',
    '2020-04-05 00:00:00'
);

INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	10,
	'19.90',
    'Lange blaue Hosen',
    '2020-07-11 12:00:00',
    '2020-04-05 00:00:00'
);

INSERT INTO dbo.Articles
(
    ArticleGroupId,
    Price,
    Name,
    ValidFrom,
    ValidTo
)
VALUES
(
	3,
	'9.90',
    'Blaue Jeans',
    '2020-10-16 12:00:00',
    '2020-04-05 00:00:00'
);

GO
UPDATE dbo.Articles 
SET ValidTo = '9999-12-31 23:59:59.9999999';
GO
ALTER TABLE dbo.Articles ADD PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo);
GO
ALTER TABLE dbo.Articles SET (SYSTEM_VERSIONING = ON
 (HISTORY_TABLE=dbo.[ArticlesHistory],DATA_CONSISTENCY_CHECK=ON)
);