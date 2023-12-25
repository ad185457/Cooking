Feature: Create Cookware

Create a new cookware

@tag1
Scenario: Create a new cookware with valid inputs
	Given Cookware with name "pot"
	And cookware with color "red"
	When Send request to create cookware
	Then Validate the created cookware's name is "pot"
	And Validate the created cookware's color is "red"

 @tag2
 Scenario: Create a new cookware with missing name
	Given Cookware without name
	And cookware with color "red"
	When Send request to create cookware
	Then Expect Error with StatusCode "400" and message "name is required"
