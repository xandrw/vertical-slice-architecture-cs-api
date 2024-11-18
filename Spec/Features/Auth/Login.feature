@Login
@SeedUsers
Feature: Login

    @LoginUnauthorized
    Scenario: Login - Unauthorized
        When I make a POST request to /api/login with the payload:
        """
        {
            "email": "invalid@email.com",
            "password": "password"
        }
        """
        Then the response status code should be 401

    @LoginUnprocessableEntity
    Scenario Outline: Login - Invalid
        When I make a POST request to /api/login with the payload:
        """
        {
            "email": <Email>,
            "password": <Password>
        }
        """
        Then the response status code should be 422
        And the response should contain
        """
        {
            "errors" : <ValidationErrors>
        }
        """
        Examples:
          | Email             | Password         | ValidationErrors                          |
          | null              | "password"       | {"email": ["error.email.required"]}       |
          | ""                | "password"       | {"email": ["error.email.required"]}       |
          | "invalid-email"   | "password"       | {"email": ["error.email.invalid"]}        |
          | "valid@email.com" | null             | {"password": ["error.password.required"]} |
          | "valid@email.com" | ""               | {"password": ["error.password.required"]} |
          | "valid@email.com" | "short"          | {"password": ["error.password.tooShort"]} |
          | "valid@email.com" | "<long_text:61>" | {"password": ["error.password.tooLong"]}  |

    @LoginOk
    Scenario: Login - Ok
        When I make a POST request to /api/login with the payload:
        """
        {
            "email": "test.admin@email.com",
            "password": "password"
        }
        """
        Then the response status code should be 200
        And the response should contain
        """
        {
            "user": {
                "id": 137,
                "email": "test.admin@email.com",
                "role": "Admin"
            }
        }
        """