﻿# API Template

This project is intended to be used as a template for creating new API endpoints.

[![SonarCloud](https://sonarcloud.io/images/project_badges/sonarcloud-white.svg)](https://sonarcloud.io/summary/new_code?id=rodabarbosa_apitemplate)

## Exception Handling

Considering performance, the API should be designed to handle exceptions using a exception filter. 
Therefor inside method you should not use the `try` and `catch` keywords and create custom exceptions if needed.

Check out the [exception filter](https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/exception-filters) for more information. Use the filter file to enter new rules if needed.

**Note:**

* The exception filter is not required for the API to work.
* Use of try/catch inside method will impact in performance.
* The use of custom exceptions is not required for the API to work.

## Consideration

This project was created using swagger as the API specification and documentation. For this reason always specify the type of the response, if not it loses its meaning.
