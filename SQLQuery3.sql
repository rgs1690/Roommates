SELECT FirstName, 
RentPortion, 
Room.Name 
FROM Roommate 
LEFT JOIN Room ON Room.Id = Roommate.RoomId 
WHERE Roommate.id = 2;