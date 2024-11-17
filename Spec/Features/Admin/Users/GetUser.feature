@SeedUsers
Feature: Get User

    @GetUserUnauthorized
    Scenario: I cannot access the GetUser endpoint anonymously
        When I make a GET request to /api/admin/users/1
        Then the response status code should be 401
    
    @GetUserForbidden
    Scenario: I cannot access the GetUser endpoint as an Author
        Given I authenticate with "test.author@example.com" and "password"
        When I make a GET request to /api/admin/users/1
        Then the response status code should be 403

    @GetUserNotFound
    Scenario: I access the GetUser endpoint as an Admin and request a user that doesn't exist
        Given I authenticate with "test.admin@email.com" and "password"
        When I make a GET request to /api/admin/users/99999
        Then the response status code should be 404

    @GetUserOk
    Scenario: I access the GetUser endpoint as an Admin
        Given I authenticate with "test.admin@email.com" and "password"
        When I make a GET request to /api/admin/users/13786
        Then the response status code should be 200
        And the response should contain
        """
        {
            "email": "test.admin@email.com",
            "role": "Admin"
        }
        """