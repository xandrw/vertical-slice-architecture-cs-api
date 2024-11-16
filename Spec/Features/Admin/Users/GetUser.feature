Feature: Get User

    Scenario: Verify the status code of a GET request
        When I make a GET request to "/api/admin/users/1"
        Then the response status code should be 200

#    Scenario: Verify the content of the response
#        When I make a GET request to "/example-endpoint"
#        Then the response status code should be 200
#        And the response content should contain "expected content"