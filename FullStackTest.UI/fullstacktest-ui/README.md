# FullStackTest UI

This Angular app fetches a list of tasks from a WebAPI, displays them, allows creating new tasks, and toggling completion by clicking a checkbox or the task name.

## Features

- Fetch tasks from a WebAPI
- Create new tasks
- Toggle completion of tasks

## How to Run

1. Navigate to the `fullstacktest-ui` folder.
2. Run `npm install` to install dependencies.
3. Run `ng serve` to start the development server.
4. Open your browser at `http://localhost:4200`.

## Customization

- Update the WebAPI URL in `src/app/tasks/tasks.service.ts` and in `src/proxy.conf.json` to match your backend.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```
## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```
Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

