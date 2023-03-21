package sdk

import (
	"context"

	"github.com/microsoft/commercial-marketplace-offer-deploy/pkg/operations"
	"github.com/microsoft/commercial-marketplace-offer-deploy/sdk/internal/generated"
)

// Performs a dry run of a deployment and returns the verification results
// returns: verification results
func (client *Client) DryRunDeployment(ctx context.Context, deploymentId int64) error {
	wait := true
	operation := generated.InvokeDeploymentOperation{
		Name:       operations.DryRunDeployment.String(),
		Parameters: nil,
		Wait:       &wait,
	}

	// TODO: implement

	_, nil := client.internalClient.InvokeDeploymentOperation(ctx, deploymentId, operation, nil)

	return nil
}

func (client *Client) StartDeployment(ctx context.Context, deploymentId int64) (string, error) {
	wait := false
	operation := generated.InvokeDeploymentOperation{
		Name:       operations.StartDeployment.String(),
		Parameters: nil,
		Wait:       &wait,
	}

	// TODO: implement

	_, nil := client.internalClient.InvokeDeploymentOperation(ctx, deploymentId, operation, nil)

	return "", nil
}

func (client *Client) CreateDeployment(ctx context.Context, request generated.CreateDeployment) (*generated.Deployment, error) {
	response, err := client.internalClient.CreateDeployment(ctx, request, nil)

	if err != nil {
		return nil, err
	}
	return &response.Deployment, nil
}

func (client *Client) ListDeployments(ctx context.Context) (generated.DeploymentManagementClientListDeploymentsResponse, error) {
	return client.internalClient.ListDeployments(ctx, nil)
}