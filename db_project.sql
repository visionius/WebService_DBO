use Movie
go
/*	winners of oscars by year	*/
create function oscars_winners(@selected_year int)
returns table
as
return
select fm.film_title, dir.director_name, dir.director_family_name, afs.winnerof, afs.winner_name from [Award_Film_Serial] afs join [Film] fm on afs.film_id = fm.film_id 
join [Director] dir on afs.director_id = dir.director_id where afs.[year] = @selected_year and afs.award_id = 1;

/*	winners of golden_glob by year	*/
create function golden_glob_winners(@selected_year int)
returns table
as
return
select ser.serial_title, dir.director_name, dir.director_family_name, afs.winnerof, afs.winner_name from [Award_Film_Serial] afs join [Director] dir on dir.director_id = afs.director_id join [Serial] ser on ser.serial_id = afs.serial_id
where afs.[year] = @selected_year and afs.award_id = 2

/*	avg users rate for films by year	*/
use Movie
go
create view film_avgRate as (select fm.film_title as film_name, avg(rt.rate) as Average_rate from [Rate] rt join [Film] fm on rt.film_id = fm.film_id  group by fm.film_title);

/*	avg users rate for serials	*/
create view serial_avgRate as (select ser.serial_title as serial_name, avg(rt.rate) as Averate_rate from [Rate] rt join [Serial] ser on ser.serial_id = rt.serial_id group by ser.serial_title);

/*	actors performances	by actor name*/
create function actor_films_serials(@actor_name nchar(30), @actor_family_name nchar(30))
returns table
as
return
select fm.film_title, gn1.genre_name as film_genre, fm.[year] as Film_year, ser.serial_title, gn2.genre_name as serial_genre , ser.[year] as Serial_year from [Actor] act full join [Film_Actor] fact on act.actor_id = fact.actor_id full join [Serial_Actor] sact on act.actor_id = sact.actor_id
full join [Film] fm on fm.film_id = fact.film_id full join [Serial] ser on ser.serial_id = sact.serial_id full join [Genre] gn1 on gn1.genre_id = fm.genre_id full join [Genre] gn2 on gn2.genre_id = ser.genre_id where lower(act.actor_name) = lower(@actor_name) and lower(act.actor_family_name) = lower(@actor_family_name);

/*	film and serial users rating	*/
create view film_serial_rating as (select usr.name, usr.family_name, ser.serial_title, fm.film_title, rt.rate from [UserTable] usr join [Rate] rt on rt.[user_id] = usr.[user_id] left join [Film] fm on fm.film_id = rt.film_id left join [Serial] ser on ser.serial_id = rt.serial_id);


/*	trigger for users	*/
create trigger tr_user_audit
on UserTable
after insert, delete
as
begin
	insert into UserAudit([user_id], name, family_name, sex, phone_number, nationality, [state], favourite_genre_id, age, type_operation, [date])
	select i.[user_id], i.name, i.family_name, i.sex,i.phone_number, i.nationality, i.[state], i.favourite_genre_id, i.age, 'INS', GETDATE() from inserted as i
	union all
	select d.[user_id], d.name, d.family_name, d.sex, d.phone_number, d.nationality, d.[state], d.favourite_genre_id, d.age, 'DEL', GETDATE() from deleted as d
end
/*	trigger for rates	*/
create trigger tr_rate_audit
on Rate
after insert, delete
as
begin
	insert into RateAudit(rate_id, [user_id], film_id, serial_id, rate, comment, type_operation, [date])
	select  i.rate_id, i.[user_id], i.film_id, i.serial_id, i.rate, i.comment, 'INS', GETDATE() from inserted as i
	union all
	select  d.rate_id, d.[user_id], d.film_id, d.serial_id, d.rate, d.comment, 'DEL', GETDATE() from deleted as d
end
/*	trigger for films	*/
create trigger tr_film_audit
on Film
after insert, delete
as
begin
	insert into FilmAudit(film_id ,film_title ,director_id ,imdb ,users_point ,story_brief ,[year] ,box_office ,budget ,genre_id ,film_duration ,type_operation,[date])
	select i.film_id ,i.film_title ,i.director_id ,i.imdb ,i.users_point ,i.story_brief ,i.[year] ,i.box_office ,i.budget ,i.genre_id ,i.film_duration, 'INS', GETDATE() from inserted as i
	union all
	select d.film_id ,d.film_title ,d.director_id ,d.imdb ,d.users_point ,d.story_brief ,d.[year] ,d.box_office ,d.budget ,d.genre_id ,d.film_duration, 'DEL', GETDATE() from deleted as d
end

/*	procedure get Movies	*/
create procedure GetMovies
as
	select fm.film_title, dr.director_name, dr.director_family_name, gn.genre_name, fm.imdb, fm.story_brief, fm.box_office, fm.film_duration from [Film] fm join [Director] dr on dr.director_id = fm.director_id join [Genre] gn on gn.genre_id = fm.genre_id
go
/*	procedure get Serials	*/
create procedure GetSerials
as
	select sr.serial_title, dr.director_name, dr.director_family_name ,sr.[year], sr.create_channel, sr.show_channel, gn.genre_name from [Serial] sr join [Director] dr on dr.director_id = sr.director_id join [Genre] gn on gn.genre_id = sr.genre_id
go
/* procedure get Users	*/
create procedure GetUsers
as
	select usr.name, usr.family_name, usr.sex, usr.phone_number, usr.nationality, usr.[state], gn.genre_name, usr.age  from [UserTable] usr join [Genre] gn on gn.genre_id = usr.favourite_genre_id
go

	
select * from oscars_winners(2018);
select * from golden_glob_winners(2016);
select * from serial_avgRate;
select * from film_avgRate;
select * from actor_films_serials('Rami', 'MaLek');
select * from actor_films_serials('viggo', 'mortensen');
select * from film_serial_rating;
exec GetUsers
exec GetMovies
select * from UserAudit;

