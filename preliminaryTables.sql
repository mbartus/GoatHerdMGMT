/*
We need a list of possible breeds.
What is Pregenancy check? 156 days, expected date, p
What is Parity? What nth time is this
What is Birthing Score? 
What is Kidding Date? Date the kid is born

Lambing date

Status code
- active, alive and in the herd, available for breeding
- sold for me
- sold for breeding
- died
- culled, no good, not healthy

How long are kids supposed to be weaned? (60 days)
Birthing Type? single, double, 
creep foster? if mother dies, will give to another mother
weaning group?
status code?
market weight, date they were sold versus 60 day weight?

When is a kid not considered a goat/sire? We need to add functionality 
to notice and transfer kids to sires/dams OR give the user the ability
to upgrade kids to sires/dams.

What are group treatment groups?

*/

DROP TABLE Treatments CASCADE;
DROP TABLE Breedings CASCADE;

DROP TABLE Births CASCADE;
DROP TABLE Children CASCADE;
DROP TABLE Animals CASCADE;

CREATE TABLE Transaction(
	pid char(5) NOT NULL,
	dop date,
	type char(1) -- i for income, e for expense
	itemdetail varchar(100),
	itemtype varchar(100),
	quantity decimal(18,4),
	unitPrice decimal(18,2),
	totalPayment decimal(18,2),
	notes varchar(1000),
	PRIMARY KEY (id) REFERENCES Purchases(pid)
)

CREATE TABLE Associates(
	aid char(5) UNIQUE NOT NULL,
	name text,
	street text,
	city text,
	state text,
	zip text,
	telephone text,
	fax text,
	email text,
	notes text,
	PRIMARY KEY (aid)
)

CREATE TABLE AnimalTreatment(
	aid char(5) UNIQUE NOT NULL,
	tid char(5) UNIQUE NOT NULL,
	date_applied date,
	details varchar(50),
	product varchar(50),
	dosage varchar(50),
	remarks varchar(50)
	PRIMARY KEY (aid, tid, date_applied),
	FOREIGN KEY (aid) REFERENCES Animals(id),
	FOREIGN KEY (tid) REFERENCES Treatments(tid),
)

CREATE TABLE Treatments(
	tid char(5) UNIQUE NOT NULL,
	product text(30),
	dosage text(30),
	remarks text(30),
	PRIMARY KEY (tid)
)

CREATE TABLE Breedings(
	id char(5) NOT NULL,
	mid char(5) NOT NULL,
	fid char(5) NOT NULL,
	mating_date date,
	remarks text(1000)
	PRIMARY KEY (id, eid, rid),
	FOREIGN KEY (mid) REFERENCES Animals(id),
	FOREIGN KEY (fid) REFERENCES Animals(id),
)

CREATE TABLE Births(
	id char(5) NOT NULL,
	mid char(5) NOT NULL,
	fid char(5) NOT NULL,
	birth_type int(2),
	birth_parity int(2),
	remarks text(1000)
	PRIMARY KEY (id, did, bid),
	FOREIGN KEY (mid) REFERENCES Animals(id),
	FOREIGN KEY (fid) REFERENCES Animals(id),
)

CREATE TABLE Children(
	species char(15),
	id char(15) UNIQUE NOT NULL,
	dob date NOT NULL,
	farm_name varchar(50) NOT NULL,
	name varchar(50),
	regulation_no char(15) UNIQUE,
	breed_code char(5) NOT NULL,
	microchip_id varchar(15),
	premise_id varchar(15), -- what is this?
	herd_id_code varchar(15), -- what is this?
	breed_registry varchar(15), -- what is this?
	sex char(1), -- M for male, F for female
	status_code varchar(10),
	disposal date, -- the day the animal dies?
	current_weight decimal(4,2),
	weight_date date,
	-- Birth, need to figure out creep foster
	weaning_weight int,
	birth_weight decimal(4,2),
	weaning_weight decimal(4,2),
	-- Market
	market_weight decimal(4,2),
	market_date date,
	remarks text(1000)
	PRIMARY KEY (id, dob) --PRIMARY KEY (id, farm_name, dob)
)

CREATE TABLE Animals(
	species char(15),
	id char(15) UNIQUE NOT NULL,
	dob date NOT NULL,
	farm_name varchar(50) NOT NULL,
	name varchar(50),
	regulation_no char(15) UNIQUE,
	breed_code char(5) NOT NULL,
	microchip_id varchar(15),
	premise_id varchar(15), -- what is this?
	herd_id_code varchar(15), -- what is this?
	breed_registry varchar(15), -- what is this?
	sex char(1), -- M for male, F for female
	status_code varchar(10),
	disposal date, -- the day the animal dies?
	current_weight decimal(4,2),
	market_weight decimal(4,2),
	market_date date,
	remarks text(1000)
	PRIMARY KEY (id, dob) --PRIMARY KEY (id, farm_name, dob)
)
