/// <reference types="cypress" />

// ***********************************************
// Custom commands for Task Management App
// ***********************************************

declare namespace Cypress {
  interface Chainable<Subject = any> {
    /**
     * Custom command to add a task
     * @example cy.addTask('My new task')
     */
    addTask(taskName: string): Chainable<Subject>
    
    /**
     * Custom command to toggle a task completion
     * @example cy.toggleTask('Task name')
     */
    toggleTask(taskName: string): Chainable<Subject>
    
    /**
     * Custom command to verify task exists
     * @example cy.verifyTaskExists('Task name')
     */
    verifyTaskExists(taskName: string): Chainable<Subject>
    
    /**
     * Custom command to verify task completion status
     * @example cy.verifyTaskCompleted('Task name', true)
     */
    verifyTaskCompleted(taskName: string, isCompleted: boolean): Chainable<Subject>
  }
}

Cypress.Commands.add('addTask', (taskName: string) => {
  cy.get('input[placeholder="New task name"]').clear().type(taskName)
  cy.get('button').contains('Add Task').should('not.be.disabled').click()
  // Wait for the task to appear in the list
  cy.contains(taskName).should('be.visible')
  // Verify input is cleared
  cy.get('input[placeholder="New task name"]').should('have.value', '')
})

Cypress.Commands.add('toggleTask', (taskName: string) => {
  cy.contains(taskName).click()
})

Cypress.Commands.add('verifyTaskExists', (taskName: string) => {
  cy.contains(taskName).should('be.visible')
})

Cypress.Commands.add('verifyTaskCompleted', (taskName: string, isCompleted: boolean) => {
  const taskElement = cy.contains(taskName)
  if (isCompleted) {
    taskElement.should('have.class', 'task-completed')
  } else {
    taskElement.should('not.have.class', 'task-completed')
  }
})
