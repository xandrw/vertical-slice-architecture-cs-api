@Health
Feature: Health Endpoint

    Scenario: Health - NoContent - SUCCESS
        When I make a GET request to /api/health
        Then the response status code should be 204