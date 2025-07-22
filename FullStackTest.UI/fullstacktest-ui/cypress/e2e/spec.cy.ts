describe('Task Management Application', () => {
  beforeEach(() => {
    // Visit the home page before each test
    cy.visit('/')
  })

  it('should display the application title', () => {
    cy.contains('Welcome to Tasks Management Application!')
    cy.contains('Tasks')
  })

  it('should display the task form', () => {
    cy.get('input[placeholder="New task name"]').should('be.visible')
    cy.get('button').contains('Add Task').should('be.visible')
  })

  it('should display task table headers', () => {
    cy.contains('Completed').should('be.visible')
    cy.contains('Task Name').should('be.visible')
  })
})
