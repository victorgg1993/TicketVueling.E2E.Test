Feature: TestOptima
	Buying round trip tickets and one way tickets with optima plan

@TestCase3_Optima
Scenario: Buy a 1 adult ticket for a round trip with optima plan
	Given the origin is Barcelona
	And the destiny is Tel Aviv
	When the ticket is bought
	Then the webpage should redirect to the schedule window

@TestCase4_Optima
Scenario: Buy a 2 adult tickets and 1 kid ticket for a one way trip with optima plan
	Given the origin is Ancona
	And the destiny is Burdeos
	When the ticket is bought
	Then the webpage should redirect to the schedule window