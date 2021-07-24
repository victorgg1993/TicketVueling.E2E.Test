Feature: TestTimeFlex
	Buying round trip ticket with timeFlex plan

@TestCase5_TimeFlex
Scenario: Buy a 1 adult ticket for a round trip with time flex plan
	Given the origin is Barcelona
	And the destiny is Tel Aviv
	When the ticket is bought
	Then the webpage should redirect to the schedule window