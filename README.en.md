# Stock Portfolio API

## Description and tasks

A developer attempted to build an API for retrieving stock portfolios, calculating their values, and providing options for deletion. Most components of the system are functional but contain errors. One suspicion is that the currency conversion is not working correctly. Unfortunately, the developer who wrote the API has gone to the Bieszczady mountains to herd sheep (apparently, he's doing well), so he won't be able to solve this problem. We're counting on you :)

The code is badly written, filled with poor decisions and unfinished logic. Your task is to complete the API project to make it work correctly.

Requirements include:
- an endpoint that returns a specific portfolio
- an endpoint that returns the value of all stocks in a given currency
- an endpoint that deletes a portfolio by soft-delete
- some unit tests (there's no need to write them for the entire project)
- currency exchange rates should be obtained dynamically from an external API - CurrencyLayer integration with a key is added to the project (can be rapleced); currency rates do not need to be retrieved every time, only every 24 hours
- NOTE: the StocksService subproject is final and cannot be modified

Additionally, we want to refactor the code as best as possible, using proper design patterns and principles of writing high-quality code. You can use additional libraries as long as they are open source.

## Database

The project uses a portable version of MongoDB to store data. You can change the database if there is a good justification for it.

## Summary

Share the modified project as a git repository, of course with a description of the changes. You can also describe additional ideas that could be implemented into the project but are not added.