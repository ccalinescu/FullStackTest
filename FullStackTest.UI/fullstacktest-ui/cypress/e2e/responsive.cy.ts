describe('Responsive Design Tests', () => {
  const viewports = [
    { device: 'desktop', width: 1280, height: 720 },
    { device: 'tablet', width: 768, height: 1024 },
    { device: 'mobile', width: 375, height: 667 }
  ]

  viewports.forEach(({ device, width, height }) => {
    describe(`${device} viewport (${width}x${height})`, () => {
      beforeEach(() => {
        cy.viewport(width, height)
        cy.visit('/')
      })

      it('should display properly on different screen sizes', () => {
        // Verify key elements are visible
        cy.contains('Welcome to Tasks Management Application!').should('be.visible')
        cy.contains('Tasks').should('be.visible')
        cy.get('input[placeholder="New task name"]').should('be.visible')
        cy.get('button').contains('Add Task').should('be.visible')
      })

      it('should allow task interaction on all devices', () => {
        const taskName = `${device} Task`
        
        cy.get('input[placeholder="New task name"]').type(taskName)
        cy.get('button').contains('Add Task').click()
        
        cy.contains(taskName).should('be.visible')
      })

      it('should maintain functionality after orientation change', () => {
        if (device === 'mobile' || device === 'tablet') {
          // Test portrait
          cy.viewport(width, height)
          cy.get('input[placeholder="New task name"]').should('be.visible')
          
          // Test landscape
          cy.viewport(height, width)
          cy.get('input[placeholder="New task name"]').should('be.visible')
        }
      })
    })
  })
})
