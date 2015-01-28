/*
We need a list of possible breeds.
What is Pregenancy check?
What is Parity?
What is Birthing Score?
What is Kidding Date?

How long are kids supposed to be weaned? (60 days)
Birthing Type?
creep foster?
weaning group?
status code?
market weight versus 60 day weight?

When is a kid not considered a goat/sire? We need to add functionality 
to notice and transfer kids to sires/dams OR give the user the ability
to upgrade kids to sires/dams.

What are group treatment groups?

*/

DROP TABLE Income CASCADE;
DROP TABLE Expenses CASCADE;
DROP TABLE Purchases CASCADE;
DROP TABLE Customers CASCADE;
DROP TABLE Treatments CASCADE;
DROP TABLE Postweanings CASCADE;
DROP TABLE Breedings CASCADE;
DROP TABLE Births CASCADE;
DROP TABLE Kids CASCADE;
DROP TABLE Sires CASCADE;
DROP TABLE Dams CASCADE;
DROP TABLE Goats CASCADE;

CREATE TABLE Income(
	cid char(5) NOT NULL,
	dop date,
	itemdetail varchar(100),
	itemtype varchar(100),
	quantity decimal(18,4),
	unitPrice decimal(18,2),
	totalPrice decimal(18,2),
	notes varchar(1000),
	PRIMARY KEY (id) REFERENCES Customers(cid)
)

CREATE TABLE Expenses(
	pid char(5) NOT NULL,
	dop date,
	itemdetail varchar(100),
	itemtype varchar(100),
	quantity decimal(18,4),
	unitPrice decimal(18,2),
	totalPayment decimal(18,2),
	notes varchar(1000),
	PRIMARY KEY (id) REFERENCES Purchases(pid)
)

CREATE TABLE Purchases(
	pid char(5) NOT NULL,
	PRIMARY KEY (pid)
)

CREATE TABLE Customers(
	cid char(5) NOT NULL,
	PRIMARY KEY (cid)
)

CREATE TABLE Treatments(
	sid char(5) NOT NULL,
	dot date,
	details varchar(50),
	product varchar(50),
	dosage varchar(50),
	remarks varchar(50)
	PRIMARY KEY (aid),
	FOREIGN KEY (aid) REFERENCES Animals(aid),
)

CREATE TABLE Postweanings(
	kid char(5) NOT NULL,
	weighdate date,
	weight decimal(4,2),
	/*
	parity
	creepfoster
	weaninggroup
	statuscode
	*/
	marketweight decimal(4,2),
	PRIMARY KEY (kid, weighdate)
	FOREIGN KEY (kid) REFERENCES Kids(kid)
)

CREATE TABLE Breedings(
	sid char(5) NOT NULL,
	mid char(5) NOT NULL,
	mdate date,
	PRIMARY KEY (sid, mid),
	FOREIGN KEY (sid) REFERENCES Sires(sid),
	FOREIGN KEY (mid) REFERENCES Dams(mid)
)

CREATE TABLE Births(
	kid char(5) NOT NULL,
	sid char(5) NOT NULL,
	mid char(5) NOT NULL,
	mdate date, 
	-- add birthing type here
	PRIMARY KEY (kid, sid, mid),
	FOREIGN KEY (kid) REFERENCES Dams(kid)
	FOREIGN KEY (sid) REFERENCES Sires(sid),
	FOREIGN KEY (mid) REFERENCES Dams(mid)
)

CREATE TABLE Kids(
	kid char(5) NOT NULL,
	reg char(5) NOT NULL,
	registry varchar(50),
	name varchar(50),
	breed varchar(50),
	dob date,
	weight decimal(4,2)
	sex char(1), --M for male, F for female
	PRIMARY KEY (kid, reg),
	FOREIGN KEY (kid, reg) REFERENCES Animals(aid, reg),
)

CREATE TABLE Sires(
	sid char(5) NOT NULL,
	reg char(5) NOT NULL,
	registry varchar(50),
	name varchar(50),
	breed varchar(50),
	dob date,
	weight decimal(4,2)
	sex char(1), --M for male, F for female
	PRIMARY KEY (sid, reg),
	FOREIGN KEY (sid, reg) REFERENCES Animals(aid, reg)
)

CREATE TABLE Dams(
	did char(5) NOT NULL,
	reg char(5) NOT NULL,
	registry varchar(50),
	name varchar(50),
	breed varchar(50),
	dob date,
	weight decimal(4,2)
	sex char(1), --M for male, F for female
	PRIMARY KEY (did, reg),
	FOREIGN KEY (did, reg) REFERENCES Animals(aid, reg)
)

CREATE TABLE Goats(
	gid char(5) NOT NULL,
	reg char(5) NOT NULL,
	registry varchar(50),
	name varchar(50),
	breed varchar(50),
	dob date,
	weight decimal(4,2)
	sex char(1), --M for male, F for female
	PRIMARY KEY (gid, reg),
	FOREIGN KEY (gid, reg) REFERENCES Animals(aid, reg)
)

CREATE TABLE Animals(
	aid char(5) NOT NULL,
	reg char(5) NOT NULL,
	registry varchar(50),
	name varchar(50),
	breed varchar(50),
	dob date,
	weight decimal(4,2)
	sex char(1), --M for male, F for female
	PRIMARY KEY (aid, reg)
)
