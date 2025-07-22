describe('API Integration Tests', () => {
  beforeEach(() => {
    // Intercept API calls and provide mock responses
    cy.intercept('GET', '/api/MyTasks', {
      statusCode: 200,
      body: [
        { id: 1, name: 'Sample Task 1', completed: false },
        { id: 2, name: 'Sample Task 2', completed: true }
      ]
    }).as('getTasks')

    cy.intercept('POST', '/api/MyTasks', {
      statusCode: 201,
      body: { id: 3, name: 'New Task', completed: false }
    }).as('createTask')

    cy.intercept('PUT', '/api/MyTasks', {
      statusCode: 200,
      body: { id: 1, name: 'Sample Task 1', completed: true }
    }).as('updateTask')

    cy.visit('/')
    cy.wait('@getTasks')
  })

  it('should load tasks from API on page load', () => {
    // Verify that mocked tasks are displayed
    cy.contains('Sample Task 1').should('be.visible')
    cy.contains('Sample Task 2').should('be.visible')
  })

  it('should send POST request when adding a new task', () => {
    const newTaskName = 'API Task' // Shorter name to fit maxlength="10"
    
    cy.get('input[placeholder="New task name"]').type(newTaskName)
    cy.get('button').contains('Add Task').click()
    
    // Verify API call was made and check the actual request structure
    cy.wait('@createTask').then((interception) => {
      expect(interception.request.body).to.have.property('name', newTaskName)
      // Note: The actual request might not include 'completed' field initially
    })
  })

  it('should send PUT request when updating a task', () => {
    // Click on a task to toggle its completion
    cy.contains('Sample Task 1').click()
    
    // Verify API call was made
    cy.wait('@updateTask').then((interception) => {
      expect(interception.request.body).to.include({
        id: 1,
        completed: true
      })
    })
  })

  it('should handle API errors gracefully', () => {
    // Intercept API call to return an error
    cy.intercept('POST', '/api/MyTasks', {
      statusCode: 500,
      body: { error: 'Internal Server Error' }
    }).as('createTaskError')

    const newTaskName = 'Error Test Task'
    
    cy.get('input[placeholder="New task name"]').type(newTaskName)
    cy.get('button').contains('Add Task').click()
    
    // Verify error message is displayed
    cy.wait('@createTaskError')
    cy.get('.error-message').should('be.visible')
    cy.contains('Error:').should('be.visible')
  })

  it('should allow dismissing error messages', () => {
    // Trigger an error first
    cy.intercept('POST', '/api/MyTasks', {
      statusCode: 400,
      body: { error: 'Bad Request' }
    }).as('createTaskError')

    cy.get('input[placeholder="New task name"]').type('Error Task')
    cy.get('button').contains('Add Task').click()
    cy.wait('@createTaskError')
    
    // Dismiss the error
    cy.get('.error-message button').click()
    
    // Verify error message is hidden
    cy.get('.error-message').should('not.exist')
  })
})
