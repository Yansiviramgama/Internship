create view rnd
as
select char  (round(65 +rand()*26,0)) as rndmstring
      SELECT round(rand()* 26,0)

    create or alter function  fn_rndnmber(@rndno int)
	returns varchar(255)
	as
	begin
	         declare @string  varchar(255)
			 declare @count int
			 set @string = ''
			 set @count = 0
			 while(@count < @rndno)
			 begin
			   set @string = @string + (select rndmstring from rnd)
			   set @count = @count + 1
			 end
			 return @string
	end
	    select dbo.fn_rndnmber(5)

	create procedure cp_insertvalue
	@rndmnumber int,
	@rndminsertnumber int
	as
	begin
	      declare @string varchar(255)
		  declare @count int
		   set @count = 0
		  set @string = (select dbo.fn_rndnmber(@rndmnumber))
		  while(@count < @rndminsertnumber)
		  begin
		   set @string = (select dbo.fn_rndnmber(@rndmnumber))
		     insert into dumy values (@string)
			 set @count = @count + 1
		  end


	end

	exec cp_insertvalue 10,1000

	create table dumy(
	  [name] varchar(255)
	)
	select * from dumy
	drop table dumy