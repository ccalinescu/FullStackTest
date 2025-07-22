describe('Task Management Features', () => {
  beforeEach(() => {
    // Mock the API endpoints before each test
    cy.intercept('GET', '/api/MyTasks', {
      statusCode: 200,
      body: []
    }).as('getTasks')

    cy.intercept('POST', '/api/MyTasks', (req) => {
      // Return a successful response with the created task
      req.reply({
        statusCode: 201,
        body: {
          id: Date.now(), // Generate a unique ID
          name: req.body.name,
          completed: false
        }
      })
    }).as('createTask')

    cy.intercept('PUT', '/api/MyTasks', (req) => {
      req.reply({
        statusCode: 200,
        body: req.body
      })
    }).as('updateTask')

    cy.visit('/')
    cy.wait('@getTasks')
  })

  describe('Task Creation', () => {
    it('should add a new task with mocked API', () => {
      const taskName = 'Test Task'
      
      cy.get('input[placeholder="New task name"]').type(taskName)
      cy.get('button').contains('Add Task').click()
      
      // Wait for the API call and verify task appears
      cy.wait('@createTask')
      cy.contains(taskName).should('be.visible')
    })

    it('should not allow adding tasks with less than 3 characters', () => {
      cy.get('input[placeholder="New task name"]').type('ab')
      cy.get('input[placeholder="New task name"]').blur()
      cy.get('button').contains('Add Task').should('be.disabled')
      
      // Check for validation message
      cy.contains('Task name must be at least 3 characters long').should('be.visible')
    })

    it('should not allow adding empty tasks', () => {
      cy.get('input[placeholder="New task name"]').type('a').clear().blur()
      cy.get('button').contains('Add Task').should('be.disabled')
      
      // Check for validation message
      cy.contains('Task name is required').should('be.visible')
    })

    it('should clear input after adding a task', () => {
      const taskName = 'ClearTest'
      
      cy.get('input[placeholder="New task name"]').type(taskName)
      cy.get('button').contains('Add Task').click()
      
      // Wait for API call and check input is cleared
      cy.wait('@createTask')
      cy.get('input[placeholder="New task name"]').should('have.value', '')
    })
  })

  describe('Task List Display with Mock Data', () => {
    it('should display multiple tasks', () => {
      const tasks = ['First', 'Second', 'Third']
      
      tasks.forEach(task => {
        cy.get('input[placeholder="New task name"]').type(task)
        cy.get('button').contains('Add Task').click()
        cy.wait('@createTask')
      })
      
      // Verify all tasks are displayed
      tasks.forEach(task => {
        cy.contains(task).should('be.visible')
      })
    })

    it('should show proper table structure after adding tasks', () => {
      // Add a task first
      cy.get('input[placeholder="New task name"]').type('Structure')
      cy.get('button').contains('Add Task').click()
      cy.wait('@createTask')
      
      // Verify table structure
      cy.get('.tasks-table').should('exist')
      cy.get('.table-header').should('exist')
      cy.get('.table-body').should('exist')
      cy.get('.table-row').should('exist')
    })
  })
})
