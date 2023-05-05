# PromptQueries

![https://img.shields.io/badge/License-MIT-yellow.svg](https://img.shields.io/badge/License-MIT-yellow.svg)

PromptQueries is a C# library that provides an easy way to execute complex queries on collections of objects using OpenAI's GPT-3 language model. With this library, you can query your collections using natural language queries and get accurate and relevant results.

## **Installation**

To use PromptQueries in your .NET project, you can install it from NuGet. Just run the following command in your NuGet package manager console:

```
mathematicaCopy code
Install-Package PromptQueries
```

## **Usage**

### **Authenticating your OpenAI API key**

Before you can use PromptQueries to execute queries, you need to authenticate your OpenAI API key. You can do this by calling the **`AddOpenAiAuth`** method when configuring your application's services:

```
csharpCopy code
using Queries.Ai;
using Microsoft.Extensions.DependencyInjection;

// ...

services.AddOpenAiAuth("<your OpenAI API key>");

```

### **Executing queries**

To execute a query, you need to call the **`RunQuery`** method of the **`IQueryPrompt`** interface. Here's an example:

```
csharpCopy code
using Queries.Ai.Utils;

// ...

IQueryable<Person> people = // get your collection of people

var queryPrompt = new QueryPrompt("<your OpenAI API key>");

var result = await queryPrompt.RunQuery(people, "find all people named John");

// do something with the result

```

### **Examples**

Here are some example queries you can execute using PromptQueries:

- Find all people named John
- Find all people older than 30
- Find all people whose name starts with "J" and are older than 25

## **Contributing**

Contributions to PromptQueries are welcome! To contribute, just fork this repository, make your changes, and submit a pull request.

## **License**

PromptQueries is licensed under the MIT License.

## **Support**

If you find PromptQueries useful, consider supporting the project by making a donation via PayPal:

![https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif](https://www.paypal.com/donate/?hosted_button_id=Z6KKYZKYY25CW)