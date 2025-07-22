describe('UI Functionality Tests', () => {
  beforeEach(() => {
    cy.visit('/')
  })

  describe('Application Layout', () => {
    it('should display the main application elements', () => {
      // Check title
      cy.contains('Welcome to Tasks Management Application!').should('be.visible')
      cy.contains('Tasks').should('be.visible')
      
      // Check form elements
      cy.get('input[placeholder="New task name"]').should('be.visible')
      cy.get('button').contains('Add Task').should('be.visible')
      
      // Check table headers
      cy.contains('Completed').should('be.visible')
      cy.contains('Task Name').should('be.visible')
    })

    it('should have proper form structure', () => {
      // Check input attributes separately
      cy.get('input[placeholder="New task name"]').should('have.attr', 'required')
      cy.get('input[placeholder="New task name"]').should('have.attr', 'minlength', '3')
      cy.get('input[placeholder="New task name"]').should('have.attr', 'maxlength', '10')
    })
  })

  describe('Form Validation', () => {
    it('should disable button for empty input', () => {
      cy.get('input[placeholder="New task name"]').clear()
      cy.get('button').contains('Add Task').should('be.disabled')
    })

    it('should disable button for input less than 3 characters', () => {
      cy.get('input[placeholder="New task name"]').type('ab')
      cy.get('input[placeholder="New task name"]').blur()
      cy.get('button').contains('Add Task').should('be.disabled')
    })

    it('should enable button for valid input', () => {
      cy.get('input[placeholder="New task name"]').type('Valid Task')
      cy.get('button').contains('Add Task').should('not.be.disabled')
    })

    it('should show validation messages', () => {
      // Test required validation - need to interact with form to trigger validation
      cy.get('input[placeholder="New task name"]').type('a').clear().blur()
      cy.contains('Task name is required').should('be.visible')
      
      // Clear the error by typing valid input
      cy.get('input[placeholder="New task name"]').type('ValidTask')
      cy.contains('Task name is required').should('not.exist')
    })

    it('should show minlength validation', () => {
      cy.get('input[placeholder="New task name"]').type('ab')
      cy.get('input[placeholder="New task name"]').blur()
      cy.contains('Task name must be at least 3 characters long').should('be.visible')
    })

    it('should respect maxlength constraint', () => {
      const longText = 'This is a very long task name that exceeds the limit'
      cy.get('input[placeholder="New task name"]').type(longText)
      
      // Should only contain first 10 characters
      cy.get('input[placeholder="New task name"]').should('have.value', 'This is a ')
    })
  })

  describe('Table Structure', () => {
    it('should display table structure correctly', () => {
      cy.get('.tasks-table').should('exist')
      cy.get('.table-header').should('exist')
      cy.get('.table-body').should('exist')
      
      // Check header columns
      cy.get('.table-header .column-completed').should('contain', 'Completed')
      cy.get('.table-header .column-task-name').should('contain', 'Task Name')
    })
  })

  describe('Error Handling UI', () => {
    it('should not show error message initially', () => {
      cy.get('.error-message').should('not.exist')
    })

    // We can simulate an error state by triggering a failed API call
    it('should handle button click when backend is unavailable', () => {
      // Type a valid task name
      cy.get('input[placeholder="New task name"]').type('Test Task')
      
      // Click the button - this will fail due to no backend, but should not crash the UI
      cy.get('button').contains('Add Task').click()
      
      // The app should handle the error gracefully
      // Either show an error message or maintain UI state
      cy.get('input[placeholder="New task name"]').should('exist') // UI should remain responsive
    })
  })
})
