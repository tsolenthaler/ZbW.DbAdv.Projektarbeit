﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class SqlRawCommands
    {
        public static string SeedArticle => @"USE AuftragsverwaltungHistory;
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
                        )";

        public static string SeedCustomer => @"USE AuftragsverwaltungHistory;
                        ALTER TABLE dbo.Customer SET (SYSTEM_VERSIONING = OFF);
                        GO
                        ALTER TABLE dbo.Customer DROP PERIOD FOR SYSTEM_TIME;
                        GO
                        
                        UPDATE dbo.Customer
                        SET Company = 'Isernet AG', ValidFrom = dateadd(DD, -30, cast(getdate() as date))
                        WHERE Company = 'Isernet AG';                 
                        GO

                        UPDATE dbo.Customer
                        SET ValidTo = '9999-12-31 23:59:59.9999999';
                        GO

                        ALTER TABLE dbo.Customer ADD PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo);
                        GO
                        ALTER TABLE dbo.Customer SET (SYSTEM_VERSIONING = ON
                         (HISTORY_TABLE=dbo.[CustomerHistory],DATA_CONSISTENCY_CHECK=ON)
                        )";

        public static string SeedAddresses => @"USE AuftragsverwaltungHistory;
                        ALTER TABLE dbo.Addresses SET (SYSTEM_VERSIONING = OFF);
                        GO
                        ALTER TABLE dbo.Addresses DROP PERIOD FOR SYSTEM_TIME;
                        GO
                        
                        UPDATE dbo.Addresses
                        SET ValidFrom = dateadd(DD, -30, cast(getdate() as date))
                        WHERE Street = 'Aprastrasse';                        

                        GO
                        UPDATE dbo.Addresses
                        SET ValidTo = '9999-12-31 23:59:59.9999999';
                        GO
                        ALTER TABLE dbo.Addresses ADD PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo);
                        GO
                        ALTER TABLE dbo.Addresses SET (SYSTEM_VERSIONING = ON
                         (HISTORY_TABLE=dbo.[AddressesHistory],DATA_CONSISTENCY_CHECK=ON)
                        )";

        public static string YearEndingRequest => @"USE AuftragsverwaltungHistory;

                        Declare @Y1 int = 2019
                        Declare @Y2 int = 2022

                        ;WITH 
	                        --Data needed to make all requests 
	                        CTE_DataForYearEndOrderSummary (OrderId, ArticleId, Quantity, Price, OrderDate, CustomerId, Quarter, Year, SumArticleQtyPerOrder, SumSalesPerCustomerPerQuarter)
		                        AS (
			                        SELECT op.OrderId, op.ArticleId, op.Quantity, a.Price, o.Date, o.CustomerId, DATEPART(QUARTER, o.Date) AS Quarter, DATEPART(YEAR,o.Date) AS Year, 
				                        SUM(Quantity) OVER (PARTITION BY op.OrderId ORDER BY op.OrderId) AS ArticleQtySumPerOrder,
				                        SUM(Price * Quantity) OVER (PARTITION BY CustomerId ORDER BY DATEPART(YEAR,o.Date), DATEPART(QUARTER, o.Date)) AS SalesPerCustomerPerQuarter

			                        FROM OrderPositions AS op
			                        INNER JOIN Articles AS a
			                        ON op.ArticleId = a.Id
			                        INNER JOIN Orders AS o
			                        ON op.OrderId = o.Id  
		                        ),
	                        --Generates list of 3 years with quarters depending on declared values @Y1 and @Y2 above (Code taken from stack overflow)
	                        CTE_Last3YearsWithQuarters (Year, Quarter)
		                        AS (
			                        Select *
			                        From ( Select Top (@Y2-@Y1+1) Year=@Y1-1+Row_Number() Over (Order By (Select NULL)) From master..spt_values n1 ) A
			                        Cross Join (values (1),(2),(3),(4)) B([Quarter])
			                        ),
	                        --Calculates Umsatztotal using GROUP BY
	                        CTE_Umsatztotal (Year, Quarter, Umsatztotal)
		                        AS (
			                        SELECT l3y.Year, l3y.Quarter, SUM(Price * Quantity) AS Umsatztotal 
			                        FROM CTE_Last3YearsWithQuarters AS l3y
			                        LEFT JOIN CTE_DataForYearEndOrderSummary AS data
			                        ON l3y.Year = data.Year AND l3y.Quarter = data.Quarter
			                        GROUP BY l3y.Year, l3y.Quarter
			                        ),
	                        CTE_AllArticlesWithValidQuarters 
		                        AS (
			                        SELECT *, DATEPART(QUARTER, ValidFrom) AS QuarterValidFrom, DATEPART(YEAR,ValidFrom) AS YearValidFrom, DATEPART(QUARTER, ValidTo) AS QuarterValidTo, DATEPART(YEAR,ValidTo) AS YearValidTo
			                        FROM Articles
			                        ),
	                        CTE_AllStatisticsExceptTotalArticles
		                        AS (
			                        SELECT *,
			                        --Order Count
			                        COUNT(*) OVER (PARTITION BY OrderId ORDER BY Year, Quarter) AS TotalOrdersPerQuarter,
			                        --Average Article Count per Order
			                        AVG(SumArticleQtyPerOrder) OVER (PARTITION BY Quarter ORDER BY Year) AS AvgArticleQtySumPerOrderPerQuarter,
			                        --Average Sales per Customer
			                        AVG(SumSalesPerCustomerPerQuarter) OVER (PARTITION BY Quarter ORDER BY Year) AS AvgSumSalesPerCustomerPerQuarter,
			                        --Total Sales
			                        SUM(Price * Quantity) OVER (PARTITION BY Quarter ORDER BY YEAR) AS SalesTotalPerQuarter
			                        FROM CTE_DataForYearEndOrderSummary
			                        ),
	                        CTE_TotalArticlesInTheSystem
		                        AS (
			                        SELECT * , COUNT(*) OVER (PARTITION BY QuarterValidFrom ORDER BY YearValidFrom) AS TotalArticlesInTheSystem
			                        FROM CTE_AllArticlesWithValidQuarters
			                        ),
	                        CTE_AllStatisticsBeforeGrouping
		                        AS (
			                        SELECT l3y.Year, l3y.Quarter, TotalOrdersPerQuarter, AvgArticleQtySumPerOrderPerQuarter, AvgSumSalesPerCustomerPerQuarter, SalesTotalPerQuarter, TotalArticlesInTheSystem
			                        FROM CTE_AllStatisticsExceptTotalArticles AS a
			                        LEFT JOIN CTE_TotalArticlesInTheSystem AS b
				                        ON a.Year = b.YearValidFrom AND a.Quarter = b.QuarterValidFrom
			                        RIGHT JOIN CTE_Last3YearsWithQuarters AS l3y
				                        ON l3y.Year = a.Year AND l3y.Quarter = a.Quarter
			                        ),
	                        CTE_AllStatisticsGrouped
		                        AS (
			                        --The average function is only used to allow the grouping to work - not nice, but it works ( - SELECT * FROM CTE_AllStatistics - shows that data was properly aggregated already before)
			                        SELECT AVG(Year) AS Year, AVG(Quarter) AS Quarter, AVG(TotalOrdersPerQuarter) AS TotalOrdersPerQuarter, AVG(AvgArticleQtySumPerOrderPerQuarter) AS AvgArticleQtySumPerOrderPerQuarter, AVG(AvgSumSalesPerCustomerPerQuarter) AS AvgSumSalesPerCustomerPerQuarter, AVG(SalesTotalPerQuarter) AS SalesTotalPerQuarter, AVG(TotalArticlesInTheSystem) AS TotalArticlesInTheSystem 
			                        FROM CTE_AllStatisticsBeforeGrouping
			                        GROUP BY Year, Quarter
			                        ),
	                        CTE_AllStatisticsGroupedRollSumArticles
		                        AS (
			                        SELECT Year, Quarter, TotalOrdersPerQuarter, AvgArticleQtySumPerOrderPerQuarter, AvgSumSalesPerCustomerPerQuarter, SalesTotalPerQuarter, SUM(TotalArticlesInTheSystem) OVER (ORDER BY Year, Quarter) AS RollSumArticlesInTheSystem 
			                        FROM CTE_AllStatisticsGrouped
			                        )

                        SELECT * FROM CTE_AllStatisticsGroupedRollSumArticles			
                        ORDER BY Year DESC, Quarter DESC";


    }
}
