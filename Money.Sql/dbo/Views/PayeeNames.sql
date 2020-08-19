CREATE VIEW [dbo].[PayeeNames]
	AS Select p.Name As CorrectName, a.Name As OriginalName From PayeeAlternateNames a INNER JOIN Payees p ON p.Id = a.PayeeId