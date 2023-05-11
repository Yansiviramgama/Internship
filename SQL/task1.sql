
USE YansiViramgama325

----- CREATE TABLES ----

-----actor:


CREATE TABLE Actor (
	Actor_ID INT IDENTITY(1,1) PRIMARY KEY,
	Actor_First_Name VARCHAR(200),
	Actor_Last_Name VARCHAR(200),
	Actor_Gender VARCHAR(200)
)
INSERT INTO Actor VALUES
('Dyanne', 'Weine', 'Female'),
('Agatha', 'Brecknock', 'Male'),
('Stesha', 'Worters', 'Female'),
('Dania', 'Zanneli', 'Female'),
('Ardelia', 'Flamank', 'Female'),
('Hollis', 'Ferschke', 'Female'),
('Nye', 'Worstall', 'Male'),
('Thomasin', 'Cullinan', 'Female'),
('Thibaud', 'Burkill', 'Male'),
('Lance', 'Ochiltree', 'Male')

SELECT * FROM Actor 

-----genres:

CREATE TABLE Genres(
	Gen_ID INT IDENTITY(1,1) PRIMARY KEY,
	Gen_Title VARCHAR(200)
)
INSERT INTO Genres VALUES 
('Action'),
('Adventure'),
('Comedy'),
('Crime '),
('mystery'),
('Fantasy'),
('Historical'),
('Horror'),
('Romance'),
('Satire')

SELECT * FROM Genres

-----director:

CREATE TABLE Director(
	Dir_ID INT IDENTITY(1,1) PRIMARY KEY,
	Dir_First_Name VARCHAR(200),
	Dir_Last_Name VARCHAR(200)
)
INSERT INTO Director VALUES
('Lind', 'Duran'),
('Travus', 'Cleveland'),
('Ermanno', 'Windas'),
('Peggi', 'Grigori'),
('Nert', 'Keesman'),
('Godart', 'Beart'),
('Basia', 'Armin'),
('Em', 'Figurski'),
('Fritz', 'Radenhurst'),
('Estrella', 'Brimman')

SELECT * FROM Director

-----movie:

CREATE TABLE Movie(
	Movie_ID INT IDENTITY(1,1) PRIMARY KEY,
	Movie_Title VARCHAR(200),
	Movie_Year DATE,
	Movie_Time TIME,
	Movie_Language VARCHAR(200),
	Movie_release_date DATE,
	Movie_release_Country VARCHAR(200)
)
INSERT INTO Movie VALUES
('Braveheart', '2015-04-10','02:48:00','Telugu','2000-05-20','India'),
('RAW', '2023-04-10','02:18:00','Hindi','2023-05-20','India'),
('War', '2022-04-10','02:00:00','English','2023-05-20','India'),
('Saaho', '2021-04-10','02:30:00','Telugu','2023-05-20','India'),
('Amadeus', '2020-04-10','03:00:00','English','2023-05-20','India'),
('Eyes Wide Shut', '2020-04-10','03:00:00','English','2023-05-20','India'),
('Chinatown', '2021-04-10','02:58:00','Hindi','2023-05-20','India'),
('Aliens', '2023-04-10','02:08:00','Telugu','2023-05-20','India')
,('Avatar', '2012-04-10','02:28:00','Hindi','2023-05-20','India')
,('Spirited Away', '2013-04-10','02:38:00','English','2023-05-20','India')
,('Braveheart', '2015-04-10','02:48:00','Telugu','2023-05-20','India'),
('heart', '2015-04-10','02:48:00','Telugu','2023-05-20','India')


SELECT * FROM Movie

-----reviewer:

CREATE TABLE Reviewer(
	Reviewer_ID INT IDENTITY(1,1) PRIMARY KEY,
	Reviewer_Name VARCHAR(200)
)
INSERT INTO Reviewer VALUES 
('yansi'),('Chirag'),('Bhavya'),('Hemang'),('Vidhi'),
('vishal'),('krishna'),('yash'),('Pooja'),('Rushik')

SELECT * FROM Reviewer

-----movie_genres:

CREATE TABLE Movie_Genres(
	Movie_ID INT FOREIGN KEY REFERENCES Movie(Movie_ID),
	Genres_ID INT FOREIGN KEY REFERENCES Genres(Gen_ID)
)
INSERT INTO Movie_Genres VALUES 
(1,2), 
(3,4),
(6,5),
(7,8),
(10,9),
(2,1),
(3,4),
(5,6),
(7,8),
(9,10)

SELECT * FROM Movie_Genres


-----movie_direction:

CREATE TABLE Movie_Direction(
	Director_ID INT FOREIGN KEY REFERENCES Director(Dir_ID),
	Movie_ID INT FOREIGN KEY REFERENCES Movie(Movie_ID)
)
INSERT INTO Movie_Direction VALUES 
(1,2), 
(3,4),
(6,5),
(7,8),
(10,9),
(2,1),
(3,4),
(5,6),
(7,8),
(9,10)

SELECT * FROM Movie_Direction


-----rating:

CREATE TABLE Rating(
	Movie_ID INT FOREIGN KEY REFERENCES Movie(Movie_ID),
	Reviewer_ID INT FOREIGN KEY REFERENCES Reviewer(Reviewer_ID),
	Reviewer_stars INT,
	Number_of_Rating INT
)
INSERT INTO Rating VALUES
(1,2,3,4), 
(3,4,6,5),
(6,5,7,8),
(7,8,10,9),
(10,9,2,1),
(2,1,3,4),
(3,4,5,6),
(5,6,7,8),
(7,8,9,10),
(9,10,9,10)

SELECT * FROM Rating
-----movie_cast:

CREATE TABLE Movie_Cast(
	Actor_ID INT FOREIGN KEY REFERENCES Actor(Actor_ID),
	Movie_ID INT FOREIGN KEY REFERENCES Movie(Movie_ID),
	[Role] VARCHAR(200)
)
INSERT INTO Movie_Cast VALUES
(1,2,'Hero'), 
(3,4,'Heroine'),
(6,5,'Villan'),
(7,8,'Dancer'),
(10,9,'Co-Actor'),
(2,1,'Hero'),
(3,4,'Heroine'),
(5,6,'Villan'),
(7,8,'Dancer'),
(9,10,'Co-Actor')

SELECT * FROM Movie_Cast

---1) From the following table, write a SQL query to find the name and year of the movies. Return movie title, movie release year

SELECT M.Movie_Title,YEAR(M.Movie_release_date) AS release_year 
FROM Movie M 

--2. From the following table, write a SQL query to find when the movie 'American Beauty' released. Return movie release year

SELECT M.Movie_Title,YEAR(M.Movie_release_date) AS release_year 
FROM Movie M WHERE M.Movie_Title = 'Avatar'

--3. From the following table, write a SQL query to find the movie that was released in 1999. Return movie title.

SELECT M.Movie_Title,YEAR(M.Movie_release_date) AS release_year 
FROM Movie M WHERE YEAR(M.Movie_release_date) = '2023' 
--
--4. From the following table, write a SQL query to find those movies, which were released before 1998. Return movie title.

SELECT M.Movie_Title,YEAR(M.Movie_release_date) AS release_year 
FROM Movie M WHERE YEAR(M.Movie_release_date) < '2023'
--
--5. From the following tables, write a SQL query to find the name of all reviewers and movies together in a single list.

SELECT M.Movie_Title,Re.Reviewer_Name FROM Movie M 
INNER JOIN Rating R ON R.Movie_ID = M.Movie_ID 
INNER JOIN Reviewer RE ON RE.Reviewer_ID = R.Reviewer_ID 

--6. From the following table, write a SQL query to find all reviewers who have rated seven or more stars to their rating. Return reviewer name.

SELECT r.Reviewer_Name, RA.Reviewer_stars FROM Reviewer R 
INNER JOIN Rating RA ON R.Reviewer_ID = RA.Reviewer_ID 
WHERE RA.Reviewer_stars > 7

--7. From the following tables, write a SQL query to find the movies without any rating. Return movie title.

SELECT M.Movie_Title FROM Movie M 
WHERE M.Movie_ID 
NOT IN ( SELECT R.Movie_ID FROM Rating R ) 

--8. From the following table, write a SQL query to find the movies with ID 905 or 907 or 917. Return movie title.

SELECT M.Movie_Title FROM Movie M 
WHERE M.Movie_ID = 9 OR M.Movie_ID = 5 OR M.Movie_ID = 7

--9. From the following table, write a SQL query to find the movie titles that contain the word 'Boogie Nights'. 
--Sort the result-set in ascending order by movie year. Return movie ID, movie title and movie release year. 

SELECT M.Movie_Title, M.Movie_ID,YEAR(M.Movie_release_date) AS release_year 
FROM Movie M WHERE M.Movie_Title LIKE '%Brave%' 
ORDER BY M.Movie_Year 

--10. From the following table, write a SQL query to find those actors with the first name 'Woody' and the last name 'Allen'. Return actor ID

SELECT A.Actor_ID FROM Actor A 
WHERE A.Actor_First_Name = 'Dania' AND A.Actor_Last_Name =  'Zanneli'


--11. get directors who have directed movies with avrage rating higher then 5

SELECT D.Dir_First_Name FROM Director D 
INNER JOIN Movie_Direction MD
ON D.Dir_ID = MD.Director_ID
INNER JOIN Movie M 
ON M.Movie_ID = MD.Movie_ID 
INNER JOIN Rating R 
ON R.Movie_ID = M.Movie_ID 
GROUP BY D.Dir_First_Name 
HAVING AVG(R.Number_of_Rating)>5

--12. get all actors who have worked for movies that were directed by specific director
--
SELECT DISTINCT A.Actor_First_Name +' '+a.Actor_Last_Name  FROM Actor A 
INNER JOIN Movie_Cast MC
ON MC.Actor_ID = A.Actor_ID
INNER JOIN Movie M 
ON M.Movie_ID = MC.Movie_ID
INNER JOIN Movie_Direction MD
ON MD.Movie_ID = M.Movie_ID
INNER JOIN Director D 
ON D.Dir_ID = MD.Director_ID 
WHERE D.Dir_First_Name = 'Ermanno'

--13. create a stored proc to get list of movies which is 3 years old and having rating greater than 5
--
CREATE PROC Sp_GetMovies 
AS
BEGIN 
	SELECT M.Movie_Title FROM Movie M 
	INNER JOIN Rating R 
	ON R.Movie_ID = M.Movie_ID
	WHERE 2023 - YEAR(M.Movie_Year) = 3
	GROUP BY M.Movie_Title
	HAVING AVG(R.Number_of_Rating)>5
END 
EXEC Sp_GetMovies

--14. create a stored proc to get list of all directors who have directed more then 2 movies
--
CREATE PROC Sp_Director
AS
BEGIN 
	SELECT D.Dir_First_Name FROM Director D 
	INNER JOIN Movie_Direction MD 
	ON MD.Director_ID = D.Dir_ID 
	GROUP BY D.Dir_First_Name 
	HAVING COUNT(MD.Movie_ID) >= 2
END
EXEC Sp_Director

--15. create a stored proc to get list of all directors which have directed a movie which have rating greater than 3.
--
CREATE PROC Sp_GetDirector
AS
BEGIN
	SELECT D.Dir_First_Name + ' '+D.Dir_Last_Name FROM Director D 
	INNER JOIN Movie_Direction MD 
	ON MD.Director_ID = D.Dir_ID
	INNER JOIN Movie M 
	ON M.Movie_ID = MD.Movie_ID 
	INNER JOIN Rating R 
	ON R.Movie_ID = M.Movie_ID
	WHERE R.Number_of_Rating > 3
END
EXEC Sp_GetDirector

--16. create a function to get worst director according to movie rating
--
ALTER FUNCTION Fn_WorstDirector()
RETURNS VARCHAR(200)
AS 
BEGIN 
RETURN
 (SELECT D.Dir_First_Name FROM Director D 
 WHERE D.Dir_ID 
 IN (SELECT MD.Director_ID FROM Movie_Direction MD
 WHERE MD.Movie_ID 
 IN(SELECT M.Movie_ID FROM Movie M
 WHERE M.Movie_ID
 IN(SELECT R.Movie_ID FROM Rating R 
 WHERE R.Number_of_Rating 
 IN(SELECT MIN(R.Number_of_Rating) FROM Rating R)))))	
END 
SELECT dbo.Fn_WorstDirector()

--17.  create a function to get worst actor according to movie rating
--
CREATE FUNCTION Fn_WrostActor()
RETURNS VARCHAR(200)
AS
BEGIN
	RETURN
	 (SELECT D.Dir_First_Name FROM Director D 
	 WHERE D.Dir_ID 
	 IN (SELECT MD.Director_ID FROM Movie_Direction MD
	 WHERE MD.Movie_ID 
	 IN(SELECT M.Movie_ID FROM Movie M
	 WHERE M.Movie_ID
	 IN(SELECT R.Movie_ID FROM Rating R 
	 WHERE R.Number_of_Rating 
	 IN(SELECT MIN(R.Number_of_Rating) FROM Rating R)))))
END

SELECT dbo.Fn_WrostActor()
--18. create a parameterized stored procedure which accept genre and give movie accordingly 

CREATE PROC Sp_Genre_GetMovie
(@Genre VARCHAR(200))
AS
BEGIN
	SELECT M.Movie_Title FROM Movie M 
	INNER JOIN Movie_Genres MG 
	ON MG.Movie_ID = M.Movie_ID 
	INNER JOIN Genres G 
	ON G.Gen_ID = MG.Genres_ID
	WHERE G.Gen_Title = @Genre
END
EXEC Sp_Genre_GetMovie 'Action'

--19. get list of movies that start with 'a' and end with letter 'e' and movie released before 2015
--
SELECT M.Movie_Title FROM Movie M 
WHERE M.Movie_Title 
LIKE 'a%e' 
AND 
YEAR(M.Movie_release_date) < 2015

SELECT M.Movie_Title,M.Movie_release_date 
FROM Movie M 
WHERE M.Movie_Title 
LIKE 'a%s' 
AND 
YEAR(M.Movie_release_date) <= 2023

--20. get a movie with highest movie cast

SELECT [H].[Movie_ID],M.[Movie_Title] FROM 
	(SELECT COUNT(MC.Actor_ID) AS [count3],
	MC.Movie_ID 
	FROM Movie_Cast MC 
	GROUP BY MC.Movie_ID  
	HAVING 
	COUNT(MC.Actor_ID) =(SELECT MAX([G].[count])
						 FROM 
						  (SELECT COUNT(MC.Actor_ID) AS [count],
						  MC.Movie_ID 
						  FROM Movie_Cast MC 
						  GROUP BY Mc.Movie_ID)[G]
						 )
	)[H] 
	INNER JOIN Movie M 
	ON [H].[Movie_ID] = [M].[Movie_ID]

--21. create a function to get reviewer that has rated highest number of movies

ALTER FUNCTION Fn_HighestNumMovie(@ID INT)
RETURNS VARCHAR(200)
AS
BEGIN
RETURN(
	SELECT DISTINCT RE.Reviewer_Name FROM (SELECT COUNT(R.Reviewer_ID) As Review,R.Movie_ID 
	FROM Rating R 
	GROUP BY R.Movie_ID
	HAVING 
	COUNT(R.Reviewer_ID)=(SELECT MAX([Main].Review) 
	FROM 
	(SELECT COUNT(R.Reviewer_ID) As Review,R.Movie_ID 
	FROM Rating R 
	GROUP BY R.Movie_ID)[Main]))[Table] 
	INNER JOIN Rating R ON R.Movie_ID = [Table].Movie_ID
	INNER JOIN Reviewer RE ON RE.Reviewer_ID = R.Reviewer_ID WHERE @ID = RE.Reviewer_ID)
END
SELECT dbo.Fn_HighestNumMovie(4)

--
--22. From the following tables, write a query in SQL to generate a report, which contain the fields movie title, name of the female actor, year of the movie, role, movie genres, the director, date of release, and rating of that movie.

SELECT 
M.Movie_Title,
A.Actor_First_Name +' '+A.Actor_Last_Name [female actor],
YEAR(M.Movie_Year) [YEAR],
MC.Role,
G.Gen_Title,
D.Dir_First_Name +' '+D.Dir_Last_Name [director Name],
M.Movie_release_date,
R.Number_of_Rating
FROM Movie M 
INNER JOIN Rating R ON M.Movie_ID = R.Movie_ID
INNER JOIN Movie_Genres MG ON MG.Movie_ID = M.Movie_ID
INNER JOIN Movie_Direction MD ON MD.Movie_ID = M.Movie_ID
INNER JOIN Movie_Cast MC ON MC.Movie_ID = M.Movie_ID
INNER JOIN Actor A ON A.Actor_ID = MC.Actor_ID
INNER JOIN Director D ON D.Dir_ID = MD.Director_ID
INNER JOIN Genres G ON G.Gen_ID = MG.Genres_ID WHERE A.Actor_Gender = 'Female'

--23. From the following tables, write a SQL query to find the years when most of the ‘Mystery Movies’ produced. Count the number of generic title and compute their average rating. Group the result set on movie release year, generic title. Return movie year, generic title, number of generic title and average rating.
--
SELECT YEAR(M.Movie_release_date) [Movie_Year],
G.Gen_Title,
COUNT(G.Gen_Title) [Number_GenTitle],
AVG(R.Number_of_Rating)[AVG_Rating]
FROM 
Movie M 
INNER JOIN Movie_Genres MG ON MG.Movie_ID = M.Movie_ID
INNER JOIN Genres G ON G.Gen_ID = MG.Genres_ID 
INNER JOIN Rating R ON R.Movie_ID = M.Movie_ID
WHERE G.Gen_Title ='mystery' GROUP BY M.Movie_release_date,G.Gen_Title

--24.  From the following tables, write a SQL query to find the highest-rated ‘Mystery Movies’. Return the title, year, and rating
SELECT
MAX(G.Gen_Title),
YEAR(M.Movie_Year),
R.Number_of_Rating
FROM 
Movie M 
INNER JOIN Movie_Genres MG ON MG.Movie_ID = M.Movie_ID
INNER JOIN Genres G ON G.Gen_ID = MG.Genres_ID 
INNER JOIN Rating R ON R.Movie_ID = M.Movie_ID
WHERE G.Gen_Title ='mystery' GROUP BY G.Gen_Title,M.Movie_Year,R.Number_of_Rating

--25. create a function which accepts genre and suggests best movie according to ratings 

ALTER FUNCTION Fn_GET_MOVIE(@Genre VARCHAR(200))
RETURNS VARCHAR(200)
AS
BEGIN
	declare @any varchar(255);
	WITH FIRST_TABLE AS
	(
		select M.*,R.Reviewer_stars, R.Number_of_Rating from Movie M, Rating R, Genres G, Movie_Genres MG where M.Movie_ID = R.Movie_ID and G.Gen_ID = MG.Genres_ID and M.Movie_ID = MG.Movie_ID and G.Gen_Title = @Genre
	),
	SECOND_TABLE as
	(
		select AVG(F.Reviewer_stars * F.Number_of_Rating) as AVGs,F.Movie_ID from FIRST_TABLE F GRoup by F.Movie_ID
	),
	MAX_AVG as
	(
		select max(AVGs) as maxy from SECOND_TABLE
	)
	select TOP 1 @any = MO.Movie_Title from SECOND_TABLE S, MAX_AVG M, Movie MO where AVGs = M.maxy and MO.Movie_ID = S.Movie_ID	
	RETURN @any; 
END
SELECT dbo.Fn_GET_MOVIE('Crime')

--26. create a function which accepts genre and suggests best director according to ratings. 

ALTER FUNCTION Fn_Get_Director(@gener VARCHAR(200))
RETURNS VARCHAR(200)
AS
BEGIN
	DECLARE @any VARCHAR(200);
	WITH Main_Table AS 
	(
		SELECT M.*,R.Reviewer_stars,R.Number_of_Rating,G.* FROM Movie M,Genres G,Movie_Genres MG,Rating R,Movie_Direction MD,Director D
		WHERE M.Movie_ID = MG.Movie_ID AND M.Movie_ID = MD.Movie_ID AND M.Movie_ID=R.Movie_ID 
		AND MG.Genres_ID = G.Gen_ID AND MD.Director_ID = D.Dir_ID AND G.Gen_Title = @gener
	),
	Rating_Table AS
	(
		SELECT AVG(MT.Reviewer_stars * MT.Number_of_Rating) AS REVIEW, MT.Movie_ID FROM Main_Table MT GROUP BY MT.Movie_ID
	),
	Max_Value AS 
	(
		select max(REVIEW) as maxvalue from Rating_Table RT
	)
	SELECT @any= M.Movie_Title FROM Rating_Table RT,Max_Value MV,Movie M WHERE REVIEW = MV.maxvalue AND M.Movie_ID = RT.Movie_ID

	RETURN @any ;
END
SELECT dbo.Fn_Get_Director  ('Action')

--27. create a function that accepts a genre and give random movie according to genre
 
ALTER FUNCTION Fn_RandomMovie(@genre VARCHAR(200))
RETURNS VARCHAR(200)
AS
BEGIN
RETURN 
ALTER VIEW RandomMovie 
AS
SELECT M.Movie_Title FROM Movie M INNER JOIN Movie_Genres MG ON MG.Movie_ID = M.Movie_ID INNER JOIN Genres G ON G.Gen_ID = MG.Genres_ID WHERE G.Gen_Title = 'Action' 
SELECT * FROM RandomMovie
										
END

SELECT dbo.Fn_RandomMovie('Action')

ALTER VIEW RandomInterger
AS
SELECT ROUND(RAND()*10,0) AS VALUE
SELECT * FROM RandomInterger


















--1. From the following table, write a SQL query to find the actors who played a role in the movie 'Annie Hall'. Return all the fields of actor table.

SELECT A.Actor_First_Name,A.Actor_Gender,A.Actor_ID,A.Actor_Last_Name FROM Actor A 
INNER JOIN Movie_Cast M ON A.Actor_ID=M.Actor_ID 
INNER JOIN Movie MO ON MO.Movie_ID = M.Movie_ID 
WHERE MO.Movie_Title = 'RAW'

--2. From the following tables, write a SQL query to find the director of a film that cast a role in 'Eyes Wide Shut'. Return director first name, last name.
--
SELECT D.Dir_First_Name,D.Dir_Last_Name,D.Dir_ID FROM Director D 
INNER JOIN Movie_Direction MD ON D.Dir_ID = MD.Director_ID  
INNER JOIN Movie M ON M.Movie_ID = MD.Movie_ID 
INNER JOIN Movie_Cast MC ON MC.Movie_ID=M.Movie_ID 
WHERE MC.Role=ANY(SELECT Movie_Cast.Role FROM Movie_Cast) AND M.Movie_Title = 'RAW'  
---
SELECT D.Dir_ID,D.Dir_First_Name,D.Dir_Last_Name
FROM Director D
WHERE D.Dir_ID 
IN (SELECT MD.Director_ID FROM Movie_Direction MD WHERE MD.Movie_ID 
IN(SELECT M.Movie_ID FROM Movie_Cast M WHERE 
M.Role = ANY(SELECT MC.Role FROM Movie_Cast MC WHERE MC.Movie_ID 
IN(SELECT M.Movie_ID FROM Movie M WHERE M.Movie_Title = 'RAW'
))))

--3. From the following table, write a SQL query to find those movies that have been released in countries other than the United Kingdom. 
--Return movie title, movie year, movie time, and date of release, releasing country. 

SELECT M.Movie_Title,M.Movie_Year,M.Movie_Time,M.Movie_release_date,M.Movie_release_Country
FROM Movie M WHERE M.Movie_release_Country != 'UK'

--4. From the following tables, write a SQL query to find for movies whose reviewer is unknown. 
--Return movie title, year, release date, director first name, last name, actor first name, last name

SELECT M.Movie_Title,YEAR(M.Movie_Year)AS Movie_Year,M.Movie_release_date,
	D.Dir_First_Name,D.Dir_Last_Name,
	A.Actor_First_Name,A.Actor_Last_Name
	FROM Movie M 
	INNER JOIN Rating R ON R.Movie_ID = M.Movie_ID 
	INNER JOIN Reviewer RE ON RE.Reviewer_ID = R.Reviewer_ID
	INNER JOIN Movie_Direction MD ON MD.Movie_ID = M.Movie_ID 
	INNER JOIN Director D ON D.Dir_ID = MD.Director_ID
	INNER JOIN Movie_Cast MC ON MC.Movie_ID = M.Movie_ID
	INNER JOIN Actor A ON A.Actor_ID = MC.Actor_ID 
	WHERE RE.Reviewer_Name = NULL

--5. From the following tables, write a SQL query to find those movies directed by the director whose first name is Woddy and last name is Allen. 
--Return movie title. 

SELECT M.Movie_Title FROM Director D 
INNER JOIN Movie_Direction MD ON MD.Director_ID = D.Dir_ID 
INNER JOIN Movie M ON M.Movie_ID = MD.Movie_ID 
WHERE D.Dir_First_Name = 'Lind' AND D.Dir_Last_Name ='Duran' 

--6. From the following tables, write a SQL query to determine those years 
--in which there was at least one movie that received a rating of at least three stars. 
--Sort the result-set in ascending order by movie year. Return movie year.

SELECT YEAR(M.Movie_Year) FROM Movie M WHERE M.Movie_ID 
IN (SELECT R.Movie_ID FROM Rating R WHERE R.Reviewer_stars > 3) 
ORDER BY YEAR(M.Movie_Year) ASC

--7. From the following table, write a SQL query to search for movies that do not have any ratings. Return movie title.

SELECT M.Movie_Title FROM Movie M INNER JOIN Rating R ON R.Movie_ID = M.Movie_ID 
WHERE R.Number_of_Rating = 1

--8. From the following table, write a SQL query to find those reviewers who have not given a rating to certain films. Return reviewer name.

SELECT R.Reviewer_Name FROM Reviewer R WHERE R.Reviewer_ID IN(SELECT RA.Reviewer_ID FROM Rating RA  WHERE RA.Reviewer_stars IS NULL )

--9. From the following tables, write a SQL query to find movies that have been reviewed by a reviewer and received a rating. 
--Sort the result-set in ascending order by reviewer name, movie title, review Stars. 
--Return reviewer name, movie title, review Stars.

SELECT RE.Reviewer_Name ,M.Movie_Title,R.Reviewer_stars FROM Movie M 
INNER JOIN Rating R ON R.Movie_ID = M.Movie_ID 
INNER JOIN Reviewer RE ON RE.Reviewer_ID = R.Reviewer_ID
WHERE R.Reviewer_stars IS NOT NULL AND RE.Reviewer_Name IS NOT NULL
ORDER BY RE.Reviewer_Name , M.Movie_Title , R.Reviewer_stars

--10. From the following table, write a SQL query to 
--find movies that have been reviewed by a reviewer and received a rating. 
--Group the result set on reviewer’s name, movie title. Return reviewer’s name, movie title.

SELECT RE.Reviewer_Name ,M.Movie_Title FROM Movie M 
INNER JOIN Rating R ON R.Movie_ID = M.Movie_ID 
INNER JOIN Reviewer RE ON RE.Reviewer_ID = R.Reviewer_ID
WHERE R.Reviewer_stars IS NOT NULL AND RE.Reviewer_Name IS NOT NULL
GROUP BY RE.Reviewer_Name , M.Movie_Title  

--11. From the following tables, write a SQL query to find those movies, 
--which have received highest number of stars. 
--Group the result set on movie title and 
--sorts the result-set in ascending order by movie title.
--Return movie title and maximum number of review stars.

SELECT M.Movie_Title,MAX(R.Reviewer_stars)  FROM Movie M INNER JOIN Rating R ON R.Movie_ID = M.Movie_ID WHERE
R.Number_of_Rating IN(SELECT MAX(R.Number_of_Rating) FROM Rating R)
GROUP BY M.Movie_Title 
ORDER BY M.Movie_Title

--12. From the following tables, 
--write a SQL query to find all reviewers who rated the movie 'American Beauty'. 
--Return reviewer name.

SELECT R.Reviewer_Name FROM Reviewer R 
WHERE R.Reviewer_ID IN(SELECT RA.Reviewer_ID FROM Rating RA 
WHERE RA.Movie_ID IN(SELECT M.Movie_ID FROM Movie M WHERE M.Movie_Title = 'Braveheart'))

--13. From the following table, 
--write a SQL query to find the movies that have not been reviewed by any reviewer body 
--other than 'Paul Monks'. 
--Return movie title.

SELECT M.Movie_Title FROM Movie M 
WHERE M.Movie_ID IN(SELECT RA.Movie_ID FROM Rating RA 
WHERE RA.Reviewer_ID NOT IN(SELECT R.Reviewer_ID FROM Reviewer R WHERE R.Reviewer_Name = 'Chirag'))

--14. From the following table, write a SQL query to find the movies with the lowest ratings. 
--Return reviewer name, movie title, and number of stars for those movies.

SELECT R.Reviewer_Name, M.Movie_Title,RA.Reviewer_stars
FROM Reviewer R, Movie M, Rating RA
WHERE RA.Reviewer_stars = (
SELECT MIN(R.Reviewer_stars)
FROM Rating R 
)
AND RA.Reviewer_ID = R.Reviewer_ID
AND RA.Movie_ID = M.Movie_ID 

--15. From the following tables, 
--write a SQL query to find the movies directed by 'James Cameron'. Return movie title.

SELECT M.Movie_Title FROM Movie M 
INNER JOIN Movie_Direction MD ON MD.Movie_ID = M.Movie_ID 
INNER JOIN Director D ON D.Dir_ID = MD.Director_ID 
WHERE D.Dir_First_Name ='Lind' AND D.Dir_Last_Name ='Duran'

--16. Write a query in SQL to 
--find the movies in which one or more actors appeared in more than one film.

SELECT M.Movie_Title 
FROM Movie M 
WHERE M.Movie_ID IN (SELECT MC.Movie_ID FROM Movie_Cast MC WHERE MC.Actor_ID 
IN (SELECT A.Actor_ID FROM Actor A WHERE A.Actor_ID 
IN (SELECT M.Actor_ID FROM Movie_Cast M 
GROUP BY M.Actor_ID 
HAVING COUNT(M.Actor_ID)>1)))