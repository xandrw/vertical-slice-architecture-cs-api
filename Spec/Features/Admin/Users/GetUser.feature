@GetUser
@SeedUsers
Feature: Get User

    @GetUserUnauthorized
    Scenario: GetUser - Unauthorized
        When I make a GET request to /api/admin/users/1
        Then the response status code should be 401
    
    @GetUserForbidden
    Scenario: GetUser - Forbidden
        Given I authenticate with "test.author@example.com" and "password"
        When I make a GET request to /api/admin/users/1
        Then the response status code should be 403

    @GetUserNotFound
    Scenario: GetUser - NotFound
        Given I authenticate with "test.admin@email.com" and "password"
        When I make a GET request to /api/admin/users/99999
        Then the response status code should be 404

    @GetUserOk
    Scenario: GetUser - Ok
        Given I authenticate with "test.admin@email.com" and "password"
        When I make a GET request to /api/admin/users/137
        Then the response status code should be 200
        And the response should contain
        """
        {
            "id": 1,
            "email": "test.admin@email.com",
            "role": "Admin"
        }
        """