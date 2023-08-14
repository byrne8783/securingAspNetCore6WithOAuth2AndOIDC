     
 SELECT u.[Id]
      ,u.[Subject]
      ,u.[UserName]
      ,u.[Password]
      ,u.[Active]
      ,uc.[Type]
      ,uc.[Value]
      --,uc.[UserId]
  FROM [Users]  u
  INNER JOIN [UserClaims] uc ON uc.[UserId] = u.Id
