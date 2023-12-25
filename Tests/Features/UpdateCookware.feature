Feature: UpdateCookware

Update a cookware

@tag1
Scenario: Update a cookware with valid inputs
	Given Cookware with name "pot"
	And cookware with color "red"
	When Send request to create cookware

	Given This cookware's updated name is "pan"
	And this cookware's updated color is "silver"
	When Send request to update this cookware
	Then Validate this cookware is updated

@tag2
Scenario: Update a cookware with missing name
	Given Cookware with name "pot"
	And cookware with color "red"
	When Send request to create cookware

	Given This cookware's updated name is missing
	And this cookware's updated color is "silver"
	When Send request to update this cookware
	Then Expect Error with StatusCode "400" and message "name is required"

@tag3
Scenario: Partially update a cookware with a valid name
	Given Cookware with name "pot"
	And cookware with color "red"
	When Send request to create cookware

	Given This cookware's new name is "pan"
	When Send request to partially update this cookware
	Then Validate this cookware is partially updated

@tag3
Scenario: Partially update a cookware with missing name
	Given Cookware with name "pot"
	And cookware with color "red"
	When Send request to create cookware

	Given This cookware's new name is missing
	When Send request to partially update this cookware
	Then Expect Error with StatusCode "400" and message "name is required"