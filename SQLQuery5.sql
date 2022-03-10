SELECT Chore.Id,
	   Name
FROM Chore
LEFT JOIN RoommateChore ON ChoreId = Chore.Id
WHERE ChoreId IS Null;


INSERT INTO RoommateChore
(ChoreId, 
RoommateId)
VALUES
(@choreId,
@roommateId);

SELECT * FROM RoommateChore;


SELECT Id, 
		CASE WHEN EXISTS (SELECT *
							FROM Chore	other 	
							LEFT JOIN RoommateChore ON ChoreId = other.Id	
							WHERE ChoreId IS NULL
								AND other.Id = c.Id)
			THEN Name + '*'
			ELSE Name
			END AS Name /* have to alias NAme or it will show up with no colum name*/
FROM Chore c


SELECT Id, 
	   Name
From Chore



