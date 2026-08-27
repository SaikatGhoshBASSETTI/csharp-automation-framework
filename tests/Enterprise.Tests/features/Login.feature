Feature: User Authentication
  As a registered user
  I want to log into the application
  So that I can access my dashboard

  @smoke
  Scenario: Successful login with valid credentials
    Given I navigate to the login page
    When I enter credentials "student" and "Password123"
    Then I should see the success header "Logged In Successfully"