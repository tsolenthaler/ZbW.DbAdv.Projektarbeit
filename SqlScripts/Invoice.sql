SELECT i.[CustomerId] CustomerId
		, c.Company as Name
		, a.Street as Street
		, a.StreetNo as StreetNo
		, a.Plz as Plz
		, a.City as City
		, a.Countryname as Countryname
		,i.[Id]
		,i.[Date] As Date
		,i.[Netto]
		,i.[Brutto]
		,i.ValidFrom as ValidFrom
		,i.ValidTo as ValidTo
  FROM [AuftragsverwaltungHistory].[dbo].[Invoices] i
  LEFT JOIN [AuftragsverwaltungHistory].[dbo].[Customer] FOR SYSTEM_TIME ALL as c
	on i.customerid = c.id
  LEFT JOIN [AuftragsverwaltungHistory].[dbo].[Addresses] FOR SYSTEM_TIME ALL as a
	on c.addressid = a.id
WHERE i.Date BETWEEN '2021-01-01' and '2022-03-06' AND c.ValidFrom <= i.Date and c.ValidTo >= i.Date AND a.ValidFrom <= i.Date and a.ValidTo >= i.Date
ORDER BY i.Date