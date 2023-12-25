Feature: Delete Cookware

Delete a cookware

@tag1
Scenario: Delete an existing cookware
	Given Cookware with name "pot"
	And cookware with color "red"
	When Send request to create cookware
	And Send request to delete this cookware
	Then Validate cookware is deleted

@tag2
Scenario: Delete a non existing cookware
	Given Cookware with name "pot"
	And cookware with color "red"
	When Send request to create cookware
	And Send request to delete this cookware
	And Send request to delete this cookware again
	Then Expect Error with StatusCode "404"
