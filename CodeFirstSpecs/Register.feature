Feature: Registration
	In order to access the website
	As a user
	I want to be able to register

Scenario: Go To Registration Page
	Given I am on the Login Page
	When I click Register
	Then I am on the Registration Page

Scenario: Fill in Registration
	Given I am on the Registration Page
	When I fill in the Form as follows
	   | Label            | Value                     |
       | User name        | jack                      |
       | Email address    | jack@user.com             |
	   | Password         | password                  |
	   | Confirm password | password                  |
	And I click the Register button
	Then I am on the Home Page Logged In
