Feature: GetCookware

Get a cookware

@tag1
Scenario: Get an existing cookware
	Given Cookware with name "pot"
	And cookware with color "red"
	When Send request to create cookware
	And Send request to get this cookware
	Then Validate the recieved cookware's name is "pot"
	And Validate the receieved cookware's color is "red"

@tag2
Scenario: Get a non existing cookware
	Given Cookware with name "pot"
	And cookware with color "red"
	When Send request to create cookware
	And Send request to delete this cookware 
	And Send request to get this cookware
	Then Expect Error with StatusCode "404"
