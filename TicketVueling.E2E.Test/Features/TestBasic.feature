Feature: TestBasic
	Buying round trip tickets and one way tickets with basic plan

@TestCase1_Basic
Scenario: Buy a 1 adult ticket for a round trip with basic plan
	Given the origin is Barcelona
	And the destiny is Tel Aviv
	When the ticket is bought
	Then the webpage should redirect to the schedule window

@TestCase2_Basic
Scenario: Buy a 2 adult tickets and 1 kid ticket for a one way trip with basic plan
	Given the origin is Ancona
	And the destiny is Burdeos
	When the ticket is bought
	Then the webpage should redirect to the schedule window