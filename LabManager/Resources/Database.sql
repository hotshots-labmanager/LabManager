--DELETE FROM HaveTutored;
DELETE FROM TutorTutoringSession;
DELETE FROM TutoringSession;
DELETE FROM Course;
DELETE FROM Tutor;

--DROP TABLE HaveTutored;
DROP TABLE TutorTutoringSession;
DROP TABLE TutoringSession;
DROP TABLE Course;
DROP TABLE Tutor;

CREATE TABLE Course (
    code VARCHAR(20) NOT NULL,
    name VARCHAR(60) NOT NULL,
    credits DECIMAL(5, 1) NOT NULL,
    numberOfStudents INT,
    CONSTRAINT pk_course PRIMARY KEY(code)
)

CREATE TABLE TutoringSession (
    code VARCHAR(20) NOT NULL,
    startTime DATETIME NOT NULL,
    endTime DATETIME NOT NULL,
    numberOfParticipants INT,
    CONSTRAINT pk_tutoringsession PRIMARY KEY (code, startTime, endTime),
    CONSTRAINT fk_tutoringsession_course FOREIGN KEY (code) REFERENCES Course(code)
	ON UPDATE CASCADE
	ON DELETE CASCADE,
	INDEX ix_tutoringsession_code NONCLUSTERED (code)
)

CREATE TABLE Tutor (
    ssn VARCHAR(20) NOT NULL,
    firstName VARCHAR(20) NOT NULL,
    lastName VARCHAR(20) NOT NULL,
    email VARCHAR(40) NOT NULL,
    password VARCHAR(256) NOT NULL,
    CONSTRAINT pk_tutor PRIMARY KEY (ssn),
    CONSTRAINT uk_tutor_email UNIQUE (email)
)

--CREATE TABLE HaveTutored (
--    ssn VARCHAR(20) NOT NULL,
--    code VARCHAR(20) NOT NULL,
--    startTime DATETIME NOT NULL,
--    endTime DATETIME NOT NULL,
--    hours DECIMAL(5, 2),
--    CONSTRAINT pk_havetutored PRIMARY KEY (ssn, code, startTime, endTime),
--    CONSTRAINT fk_havetutored_tutoringsession FOREIGN KEY (code, startTime, endTime) REFERENCES TutoringSession(code, startTIme, endTime)
--	ON UPDATE CASCADE
--	ON DELETE CASCADE,
--    CONSTRAINT fk_havetutored_tutor FOREIGN KEY (ssn) REFERENCES Tutor(ssn)
--	ON UPDATE CASCADE
--	ON DELETE CASCADE,
--	INDEX ix_havetutored_code_startTime_endTime NONCLUSTERED (code, startTime, endTime),
--	INDEX ix_havetutored_ssn NONCLUSTERED (ssn),
--	INDEX ix_havetutored_code NONCLUSTERED (code),
--	INDEX ix_havetutored_startTime NONCLUSTERED (startTime),
--	INDEX ix_havetutored_endTime NONCLUSTERED (endTime)

--)

CREATE TABLE TutorTutoringSession (
    ssn VARCHAR(20) NOT NULL,
    code VARCHAR(20) NOT NULL,
    startTime DATETIME NOT NULL,
    endTime DATETIME NOT NULL,
    CONSTRAINT pk_tutortutoringsession PRIMARY KEY (ssn, code, startTime, endTime),
    CONSTRAINT fk_tutortutoringsession_tutoringsession FOREIGN KEY (code, startTime, endTime) REFERENCES TutoringSession(code, startTIme, endTime)
	ON UPDATE CASCADE
	ON DELETE CASCADE,
    CONSTRAINT fk_tutortutoringsession_tutor FOREIGN KEY (ssn) REFERENCES Tutor(ssn)
	ON UPDATE CASCADE
	ON DELETE CASCADE,
	INDEX ix_tutortutoringsession_code_startTime_endTime NONCLUSTERED (code, startTime, endTime),
	INDEX ix_tutortutoringsession_ssn NONCLUSTERED (ssn),
	INDEX ix_tutortutoringsession_code NONCLUSTERED (code),
	INDEX ix_tutortutoringsession_startTime NONCLUSTERED (startTime),
	INDEX ix_tutortutoringsession_endTime NONCLUSTERED (endTime)
)

INSERT INTO Course VALUES ('INFC20', 'Advanced Database Systems', 7.5, 40);
INSERT INTO Course VALUES ('INFC40', 'Information Systems Security', 7.5, 60);
INSERT INTO Course VALUES ('INFC25', 'Human-Computer Interaction, Analysis', 7.5, 50);
INSERT INTO Course VALUES ('INFC35', 'Decision Support Systems', 7.5, 50);
INSERT INTO Course VALUES ('INFC50', 'Content Management Systems', 7.5, 60);

INSERT INTO Tutor VALUES ('111', 'Sebastian', 'Carlsson', 'sebastian@carlsson.com', '65535:GgGpK5bu3mps688WVX8ipMo9m93Tz77/g12Rg79yzKPzUcCywyK29MKlmRPcBVnCEwsKYi3RzrY3Zc/RMoq/qA==:xOb5v4OzYjvhAhzgYAT6CgoRu8Uj61uwBBHWhDs/58BEckN2/mu15g8+Y5XKmLEfnJ6W+JmcS1NPPMKqS0DuVQ==');
INSERT INTO Tutor VALUES ('222', 'Daniel', 'Nilsson', 'daniel@nilsson.com', '65535:x8gfiwt466Yizg+NzOC8JJ5P4x9jCz/5kjTzHbh9sYX2txLSZTLr+ENe5DUPRfpIvZNMAmmuj4t67r9cW7SXsQ==:7r8CkNi0cPFxVJoFXU7gzK7PG/MNaOA/FHBroYP6+IHlEfGWjo5TF83A5j3nAGMmEWpYD+HOk6aiJVxq+D/8YQ==');
INSERT INTO Tutor VALUES ('333', 'Joseph', 'Erterius', 'joseph@erterius.com', '65535:Q7V52F++TOuRibk4r6Av7f0QBuIIqXWvpFY633pE9/wa2X+FEud1sqIYjI5VgxDr/ijlUJ0fiVn5pm2VkyES7A==:hxsWF+JWFdnlmuiKdTobSNNmYAaMqdwOTXLwhD1qbPi91+1oXGzKZnOyGeXqupYDgvc8qrcSYTga2wGDg9ikqQ==');

INSERT INTO TutoringSession VALUES ('INFC20', '2017-10-04 08:00', '2017-10-04 10:00', 18);
INSERT INTO TutoringSession VALUES ('INFC20', '2017-10-04 10:00', '2017-10-04 12:00', 22);

INSERT INTO TutoringSession VALUES ('INFC40', '2017-10-04 08:00', '2017-10-04 10:00', 25);
INSERT INTO TutoringSession VALUES ('INFC40', '2017-10-06 08:00', '2017-10-06 10:00', 20);
INSERT INTO TutoringSession VALUES ('INFC40', '2017-10-06 10:00', '2017-10-06 12:00', 27);
INSERT INTO TutoringSession VALUES ('INFC40', '2017-10-07 08:00', '2017-10-07 10:00', 19);

INSERT INTO TutoringSession VALUES ('INFC25', '2017-10-04 13:00', '2017-10-04 15:00', 15);
INSERT INTO TutoringSession VALUES ('INFC25', '2017-10-04 15:00', '2017-10-04 17:00', 11);

INSERT INTO TutoringSession VALUES ('INFC35', '2017-10-05 10:00', '2017-10-05 12:00', 16);
INSERT INTO TutoringSession VALUES ('INFC35', '2017-10-05 13:00', '2017-10-05 15:00', 17);

INSERT INTO TutoringSession VALUES ('INFC50', '2017-10-08 08:00', '2017-10-08 12:00', 20);
INSERT INTO TutoringSession VALUES ('INFC50', '2017-10-08 13:00', '2017-10-08 17:00', 23);

INSERT INTO TutoringSession VALUES ('INFC20', '2018-12-10 08:00', '2018-12-10 10:00', null);
INSERT INTO TutoringSession VALUES ('INFC20', '2018-12-10 10:00', '2018-12-10 12:00', null);

INSERT INTO TutoringSession VALUES ('INFC25', '2018-12-10 08:00', '2018-12-10 10:00', null);
INSERT INTO TutoringSession VALUES ('INFC25', '2018-12-10 10:00', '2018-12-10 12:00', null);

INSERT INTO TutoringSession VALUES ('INFC40', '2018-12-10 08:00', '2018-12-10 10:00', null);
INSERT INTO TutoringSession VALUES ('INFC40', '2018-12-10 10:00', '2018-12-10 12:00', null);
INSERT INTO TutoringSession VALUES ('INFC40', '2018-12-12 15:00', '2018-12-12 17:00', null);
INSERT INTO TutoringSession VALUES ('INFC40', '2018-12-13 13:00', '2018-12-13 15:00', null);

INSERT INTO TutoringSession VALUES ('INFC35', '2018-12-11 10:00', '2018-12-11 12:00', null);
INSERT INTO TutoringSession VALUES ('INFC35', '2018-12-11 13:00', '2018-12-11 15:00', null);

INSERT INTO TutoringSession VALUES ('INFC50', '2018-12-14 08:00', '2018-12-14 12:00', null);
INSERT INTO TutoringSession VALUES ('INFC50', '2018-12-14 13:00', '2018-12-14 15:00', null);

INSERT INTO TutorTutoringSession VALUES ('111', 'INFC20', '2018-12-10 08:00', '2018-12-10 10:00');
INSERT INTO TutorTutoringSession VALUES ('222', 'INFC20', '2018-12-10 08:00', '2018-12-10 10:00');
INSERT INTO TutorTutoringSession VALUES ('111', 'INFC20', '2018-12-10 10:00', '2018-12-10 12:00');
INSERT INTO TutorTutoringSession VALUES ('222', 'INFC20', '2018-12-10 10:00', '2018-12-10 12:00');

INSERT INTO TutorTutoringSession VALUES ('333', 'INFC40', '2018-12-10 08:00', '2018-12-10 10:00');
INSERT INTO TutorTutoringSession VALUES ('333', 'INFC40', '2018-12-10 10:00', '2018-12-10 12:00');
INSERT INTO TutorTutoringSession VALUES ('111', 'INFC40', '2018-12-12 15:00', '2018-12-12 17:00');
INSERT INTO TutorTutoringSession VALUES ('333', 'INFC40', '2018-12-12 15:00', '2018-12-12 17:00');
INSERT INTO TutorTutoringSession VALUES ('111', 'INFC40', '2018-12-13 13:00', '2018-12-13 15:00');
INSERT INTO TutorTutoringSession VALUES ('333', 'INFC40', '2018-12-13 13:00', '2018-12-13 15:00');

INSERT INTO TutorTutoringSession VALUES ('111', 'INFC35', '2018-12-11 10:00', '2018-12-11 12:00');
INSERT INTO TutorTutoringSession VALUES ('222', 'INFC35', '2018-12-11 10:00', '2018-12-11 12:00');
INSERT INTO TutorTutoringSession VALUES ('333', 'INFC35', '2018-12-11 10:00', '2018-12-11 12:00');
INSERT INTO TutorTutoringSession VALUES ('111', 'INFC35', '2018-12-11 13:00', '2018-12-11 15:00');
INSERT INTO TutorTutoringSession VALUES ('222', 'INFC35', '2018-12-11 13:00', '2018-12-11 15:00');
INSERT INTO TutorTutoringSession VALUES ('333', 'INFC35', '2018-12-11 13:00', '2018-12-11 15:00');

INSERT INTO TutorTutoringSession VALUES ('222', 'INFC50', '2018-12-14 08:00', '2018-12-14 12:00');
INSERT INTO TutorTutoringSession VALUES ('333', 'INFC50', '2018-12-14 08:00', '2018-12-14 12:00');
INSERT INTO TutorTutoringSession VALUES ('222', 'INFC50', '2018-12-14 13:00', '2018-12-14 15:00');
INSERT INTO TutorTutoringSession VALUES ('333', 'INFC50', '2018-12-14 13:00', '2018-12-14 15:00');

--INSERT INTO HaveTutored VALUES ('111', 'INFC20', '2017-10-04 08:00', '2017-10-04 10:00', 2);
--INSERT INTO HaveTutored VALUES ('111', 'INFC20', '2017-10-04 10:00', '2017-10-04 12:00', 2);
--INSERT INTO HaveTutored VALUES ('222', 'INFC20', '2017-10-04 08:00', '2017-10-04 10:00', 2);
--INSERT INTO HaveTutored VALUES ('222', 'INFC20', '2017-10-04 10:00', '2017-10-04 12:00', 2);

--INSERT INTO HaveTutored VALUES ('333', 'INFC40', '2017-10-04 08:00', '2017-10-04 10:00', 2);
--INSERT INTO HaveTutored VALUES ('111', 'INFC40', '2017-10-06 08:00', '2017-10-06 10:00', 1.5);
--INSERT INTO HaveTutored VALUES ('111', 'INFC40', '2017-10-06 10:00', '2017-10-06 12:00', 2);
--INSERT INTO HaveTutored VALUES ('222', 'INFC40', '2017-10-06 08:00', '2017-10-06 10:00', 1.5);
--INSERT INTO HaveTutored VALUES ('222', 'INFC40', '2017-10-06 10:00', '2017-10-06 12:00', 2);
--INSERT INTO HaveTutored VALUES ('333', 'INFC40', '2017-10-06 08:00', '2017-10-06 10:00', 2);
--INSERT INTO HaveTutored VALUES ('333', 'INFC40', '2017-10-06 10:00', '2017-10-06 12:00', 2);

--INSERT INTO HaveTutored VALUES ('222', 'INFC25', '2017-10-04 13:00', '2017-10-04 15:00', 1);
--INSERT INTO HaveTutored VALUES ('222', 'INFC25', '2017-10-04 15:00', '2017-10-04 17:00', 2);
--INSERT INTO HaveTutored VALUES ('333', 'INFC25', '2017-10-04 13:00', '2017-10-04 15:00', 2);
--INSERT INTO HaveTutored VALUES ('333', 'INFC25', '2017-10-04 15:00', '2017-10-04 17:00', 1);

--INSERT INTO HaveTutored VALUES ('111', 'INFC35', '2017-10-05 10:00', '2017-10-05 12:00', 2);
--INSERT INTO HaveTutored VALUES ('111', 'INFC35', '2017-10-05 13:00', '2017-10-05 15:00', 2);

--INSERT INTO HaveTutored VALUES ('111', 'INFC50', '2017-10-08 08:00', '2017-10-08 12:00', 4);
--INSERT INTO HaveTutored VALUES ('111', 'INFC50', '2017-10-08 13:00', '2017-10-08 17:00', 4);
--INSERT INTO HaveTutored VALUES ('222', 'INFC50', '2017-10-08 08:00', '2017-10-08 12:00', 3);
--INSERT INTO HaveTutored VALUES ('222', 'INFC50', '2017-10-08 13:00', '2017-10-08 17:00', 3);
--INSERT INTO HaveTutored VALUES ('333', 'INFC50', '2017-10-08 08:00', '2017-10-08 12:00', 4);
--INSERT INTO HaveTutored VALUES ('333', 'INFC50', '2017-10-08 13:00', '2017-10-08 17:00', 2);

