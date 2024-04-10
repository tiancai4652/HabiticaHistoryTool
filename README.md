# Habitica Task Data Preservation Tool

## Introduction

The Habitica Task Data Preservation Tool is a custom solution designed to address the limitation of Habitica's default data retention policy, which only keeps track of completed tasks for up to 30 days. With this tool, users can now preserve a complete record of all their accomplished tasks indefinitely.

![image](https://github.com/tiancai4652/HabiticaHistoryTool/blob/master/110209_mosaic.png)

## Purpose

The primary purpose of this tool is to provide a reliable and convenient way to:

- Access completed task data from Habitica using the platform's API.
- Store and update local copies of the task data to ensure long-term retention.
- Regularly update the local database with new completed tasks to maintain an accurate and up-to-date record.

## How It Works

The tool utilizes the Habitica API to fetch completed tasks and then saves this information to a local database. Each time the tool is run, it retrieves the latest data from the API and updates the local storage, ensuring that the user's task history is always current and complete.

## Key Features

- **API Integration**: The tool seamlessly integrates with the Habitica API to fetch completed tasks.
- **Data Preservation**: It saves all completed tasks to a local database, bypassing the 30-day limitation.
- **Automatic Updates**: The tool automatically updates the local database with new task data upon each execution.
- **User-Friendly Interface**: The program features an intuitive interface that makes it easy for users to manage their task history.

## Usage

To use the Habitica Task Data Preservation Tool, follow these simple steps:

1. Ensure you have the necessary permissions to access the Habitica API.

2. Run the program and authorize it to access your Habitica account.

3. The tool will fetch your completed tasks and update your local database.

4. To maintain an up-to-date record, schedule the tool to run at regular intervals.

   **In fact, as long as you log in to the web :)**

## Interface Overview

The program interface is designed for clarity and ease of use. It displays key information such as:

- **API Endpoint**: The URL used to fetch the task data from Habitica.
- **Data Sources**: Sections for different types of data, such as "TodosSource," "HabitsSource," and "DailySource."
- **Task Information**: Details of each task, including "NAME," "UPDATEDDATE," and "CREATEDDATE."

## Conclusion

The Habitica Task Data Preservation Tool is an invaluable resource for users who wish to keep a comprehensive archive of their Habitica task history. By leveraging the power of the Habitica API and the convenience of local data storage, this tool ensures that your achievements are never lost, allowing you to reflect on your progress and accomplishments over time.

## Disclaimer

This tool is not affiliated with or endorsed by Habitica. It is an independent project created by a user to enhance personal data management. Use this tool at your own risk, and ensure you comply with Habitica's terms of service when using their API.

---

