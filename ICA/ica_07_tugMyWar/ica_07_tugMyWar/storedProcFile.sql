use rwatson31_tugOwar
go

drop table if exists gameState
go

CREATE TABLE [dbo].[gameState](
	[GameId] [varchar](9)
        CHECK (GameId like '[0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9]')
		CONSTRAINT UPKCL_gameId PRIMARY KEY CLUSTERED,

    player1Name       varchar(20)       NOT NULL,
	player2Name		varchar(20) not null,
	currentLocation       int       NOT NULL,
	currentTurns          int          NOT NULL

)
go


drop procedure if exists updateData
go

create procedure updateData
@turnCount int,
@currentLocation int
as
--select gS.GameId from gameState gS
begin try
update gameState
set currentLocation = @currentLocation,
	currentTurns = @turnCount
end try
begin catch
print Error_Message()
end catch
go

drop procedure if exists newGame
go

create procedure newGame
@gameId varchar(9),
@playerName1 varchar(20),
@playerName2 varchar(20),
@currentLocation int,
@turnCount int
as 
begin try
insert into gameState (player1Name,player2Name,currentLocation,currentTurns)
values(@playerName1,@playerName2,@currentLocation,@turnCount)
end try
begin catch
print Error_Message()
end catch
go
