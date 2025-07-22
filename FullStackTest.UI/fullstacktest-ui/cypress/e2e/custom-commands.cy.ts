describe('Task Management with Custom Commands', () => {
  beforeEach(() => {
    // Mock API endpoints before each test
    cy.intercept('GET', '/api/MyTasks', { statusCode: 200, body: [] }).as('getTasks')
    
    cy.intercept('POST', '/api/MyTasks', (req) => {
      req.reply({
        statusCode: 201,
        body: {
          id: Date.now(),
          name: req.body.name,
          completed: false
        }
      })
    }).as('createTask')
    
    cy.intercept('PUT', '/api/MyTasks', (req) => {
      req.reply({ statusCode: 200, body: req.body })
    }).as('updateTask')

    cy.visit('/')
    cy.wait('@getTasks')
  })

  it('should use custom commands to test task workflow', () => {
    // Test adding tasks with shorter names (respecting maxlength="10")
    cy.addTask('Learn Cy') // 8 characters
    cy.addTask('Write E2E') // 9 characters  
    cy.addTask('Deploy') // 6 characters
    
    // Verify tasks exist
    cy.verifyTaskExists('Learn Cy')
    cy.verifyTaskExists('Write E2E')
    cy.verifyTaskExists('Deploy')
    
    // Toggle task completion
    cy.toggleTask('Learn Cy')
    cy.verifyTaskCompleted('Learn Cy', true)
    
    // Toggle it back
    cy.toggleTask('Learn Cy')
    cy.verifyTaskCompleted('Learn Cy', false)
  })

  it('should handle multiple task operations efficiently', () => {
    const tasks = [
      'Task A', // 6 characters
      'Task B', // 6 characters
      'Task C', // 6 characters
      'Task D', // 6 characters
      'Task E'  // 6 characters
    ]
    
    // Add all tasks
    tasks.forEach(task => cy.addTask(task))
    
    // Verify all tasks exist
    tasks.forEach(task => cy.verifyTaskExists(task))
    
    // Complete every other task
    tasks.filter((_, index) => index % 2 === 0)
         .forEach(task => {
           cy.toggleTask(task)
           cy.verifyTaskCompleted(task, true)
         })
  })

  it('should handle edge cases with custom commands', () => {
    // Test with minimum length task
    cy.addTask('Min') // 3 characters (minimum allowed)
    cy.verifyTaskExists('Min')
    
    // Test with maximum length task
    cy.addTask('MaxLenTask') // 10 characters (maximum allowed)
    cy.verifyTaskExists('MaxLenTask')
  })
})
